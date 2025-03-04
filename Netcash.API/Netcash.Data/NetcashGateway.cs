using Microsoft.Extensions.Logging;
using Netcash.Common.Exceptions;
using Netcash.Data.Requests;
using Netcash.DTO.Enums;
using Netcash.DTO.Requests;
using Netcash.DTO.Responses;
using NIWS;
using System.Text;

namespace Netcash.Data
{
    public class NetcashGateway : INetcashGateway
    {
        private readonly INIWS_NIF _client;
        private readonly ILogger<NetcashGateway> _logger;

        public NetcashGateway(INIWS_NIF client, ILogger<NetcashGateway> logger)
        {
            _logger = logger;
            _client = client;
        }

        //https://api.netcash.co.za/value-added-services/emandate/emandate-synchronous/#Examples
        public async Task<string> RequestMandateAsync(AddMandateRequest request, string serviceKey)
        {
            try
            {
                var response = await AddMandateInternalAsync(request, serviceKey);
                if (response?.Errors?.Length > 0)
                    throw new NetcashGatewayException($"Mandate creation failed: {string.Join(";", response.Errors)}");

                return response.MandateUrl;
            }
            catch (NetcashGatewayException ex)
            {
                _logger.LogError(ex, "Error creating mandate");
                throw;
            }
            catch(ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex, "Error creating mandate");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating mandate");
                throw new NetcashGatewayException("Unable to add mandate.", ex);
            }
        }

        private async Task<AddMandateResponse> AddMandateInternalAsync(AddMandateRequest request, string serviceKey)
        {
            var response = await _client.AddMandateAsync(
                    serviceKey,
                    request.AccountReference,
                    request.MandateName,
                    request.MandateAmount,
                    request.IsConsumer,
                    request.FirstName,
                    request.Surname,
                    request.TradingName,
                    request.RegistrationNumber,
                    request.RegisteredName,
                    request.MobileNumber,
                    request.DebitFrequency,
                    request.CommencementMonth,
                    request.CommencementDay,
                    request.AgreementDate,
                    request.AgreementReferenceNumber,
                    request.CancellationNoticePeriod,
                    request.PublicHolidayOption,
                    request.Notes,
                    request.Field1,
                    request.Field2,
                    request.Field3,
                    request.Field4,
                    request.Field5,
                    request.Field6,
                    request.Field7,
                    request.Field8,
                    request.Field9,
                    request.AllowVariableDebitAmounts,
                    request.BankDetailType,
                    request.BankAccountName,
                    request.BankAccountNumber,
                    request.BranchCode,
                    request.BankAccountType,
                    request.CreditCardToken,
                    request.CreditCardType,
                    request.ExpiryMonth,
                    request.ExpiryYear,
                    request.IsIdNumber,
                    request.Title,
                    request.EmailAddress,
                    request.MobileNumber,
                    request.DateOfBirth,
                    request.CommencementDay,
                    request.DebitMasterfileGroup,
                    request.PhysicalAddressLine1,
                    request.PhysicalAddressLine2,
                    request.PhysicalAddressLine3,
                    request.PhysicalSuburb,
                    request.PhysicalCity,
                    request.PhysicalProvince,
                    request.PhysicalCity,
                    request.MandateActive,
                    request.RequestAVS,
                    request.AVSCheckNumber,
                    request.IncludeDebiCheck,
                    request.DebiCheckMandateTemplateId,
                    request.DebiCheckCollectionAmount,
                    request.DebiCheckFirstCollectionDiffers,
                    request.DebiCheckFirstCollectionDate,
                    request.DebiCheckFirstCollectionAmount,
                    request.DebiCheckCollectionDayCode,
                    request.AddToMasterFile
                    );

            return response.ErrorCode switch
            {
                "100" => throw new NetcashGatewayException("Authentication failure"),
                "200" => throw new Exception("Web service error. Contact support@netcash.co.za"),
                "203" => throw new NetcashGatewayException(
                                "Parameter error. One or more of the parameters in the string is incorrect",
                                new Exception(string.Join(",", response.Errors))
                            ),
                "000" => response,
                _ => throw new ArgumentOutOfRangeException(response.ErrorCode, response.ErrorCode, "Invalid code received from Netcash")
            };
        }

        //https://api.netcash.co.za/value-added-services/compact-debit/
        public async Task<BatchDebitOrderResponse> GenerateDebitOrderFiles(BatchDebitOrderRequest request, string serviceKey, string vendorKey)
        {
            try
            {
                var file = BuildDebitOrderBatchFile(request, serviceKey, vendorKey);
                var fileToken = await BatchFileUploadInternalAsync(serviceKey, file.ToString());
                if (string.IsNullOrEmpty(fileToken))
                    throw new NetcashGatewayException("The file upload did not return a valid file token from Netcash.");

                var fileReportResponse = await FetchBatchFileUploadReport(serviceKey, fileToken);

                return new BatchDebitOrderResponse
                {
                    FileToken = fileToken,
                    FileUploadReport = fileReportResponse.FileUploadReport,
                    FileUploadStatus = fileReportResponse.FileUploadStatus
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating batch debit order files.");
                throw new NetcashGatewayException("Unable to generate and upload debit order files. See inner exception for details.", ex);
            }
        }

        private static StringBuilder BuildDebitOrderBatchFile(BatchDebitOrderRequest request, string serviceKey, string vendorKey)
        {
            var sb = new StringBuilder();

            // Header record
            sb.Append("H\t");
            sb.Append($"{serviceKey}\t");
            sb.Append("1\t");
            sb.Append("CompactTwoDay\t");
            sb.Append($"{request.BatchReferenceNo}\t");
            sb.Append($"{request.DebitOrderDate}\t");
            sb.Append($"{vendorKey}\t");
            sb.Append("\n");

            // Key record
            sb.Append($"K\t");
            sb.Append($"{(int)NetCashKeyEnum.AccountReference}\t");
            sb.Append($"{(int)NetCashKeyEnum.Amount}\t");
            // add 301 enum code here, Extra 1 field name, should house the batch reference
            sb.Append($"\n");

            // Transaction records
            foreach (var user in request.Users)
            {
                sb.Append($"T\t");
                sb.Append($"{user.AccountNo}\t");
                sb.Append($"{user.Amount}\t");
                sb.Append($"\n");
            }

            // Footer record
            sb.Append($"F\t");
            sb.Append($"{request.Users.Count}\t");
            sb.Append($"{request.Users.Sum(x => x.Amount)}\t");
            sb.Append($"9999");

            return sb;
        }

        private async Task<string> BatchFileUploadInternalAsync(string serviceKey, string file)
        {
            var fileToken = await _client.BatchFileUploadAsync(serviceKey, file);
            return fileToken switch
            {
                "100" => throw new NetcashGatewayException("Authentication failure. Ensure that the service key in the method call is correct"),
                "101" => throw new NetcashGatewayException("Date format error. If the string contains a date, it should be in the format CCYYMMDD"),
                "102" => throw new NetcashGatewayException("Parameter error. One or more of the parameters in the string is incorrect"),
                "200" => throw new NetcashGatewayException("General code exception. Please contact Netcash Technical Support"),
                _ => fileToken,
            };
        }

        public async Task<BatchDebitOrderResponse> FetchBatchFileUploadReport(string serviceKey, string fileToken)
        {
            var response = await _client.RequestFileUploadReportAsync(serviceKey, fileToken);
            if (string.IsNullOrWhiteSpace(response))
                throw new NetcashGatewayException("Received an empty response from Netcash when fetching the file upload report.");

            string[] lines = response.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
                throw new NetcashGatewayException("Invalid report format received from Netcash.");

            var headerFields = lines[0].Split("\t");
            if (headerFields.Length < 3)
                throw new NetcashGatewayException("Unexpected report header format received from Netcash.");

            var status = headerFields[2].Trim();

            return status switch
            {
                "SUCCESSFUL" => new BatchDebitOrderResponse
                {
                    FileUploadStatus = nameof(BatchStatusEnum.Successful),
                    FileUploadReport = response
                },
                "UNSUCCESSFUL" => new BatchDebitOrderResponse
                {
                    FileUploadStatus = nameof(BatchStatusEnum.Unsuccessful),
                    FileUploadReport = response
                },
                "SUCCESSFUL WITH ERRORS" => new BatchDebitOrderResponse
                {
                    FileUploadStatus = nameof(BatchStatusEnum.SuccessfulWithErrors),
                    FileUploadReport = response
                },
                _ => throw new ArgumentOutOfRangeException(response, response, "batch status received from Netcash")
            };
        }
    }
}