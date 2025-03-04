using AutoMapper;
using Microsoft.Extensions.Logging;
using Netcash.Common.Exceptions;
using Netcash.Data;
using Netcash.Data.Requests;
using Netcash.Domain.Interfaces;
using Netcash.DTO.Requests;
using Netcash.DTO.Responses;

namespace Netcash.Domain.Services
{
    public class NetcashService : INetcashService
    {
        private readonly IMapper _mapper;
        public readonly INetcashGateway _gateway;
        public readonly ILogger<NetcashService> _logger;

        public NetcashService(IMapper mapper, INetcashGateway gateway, ILogger<NetcashService> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _gateway = gateway;
        }

        public async Task<string> RequestMandateUrl(CreateMandateRequest request, string serviceKey)
        {
            try
            {
                var netcashRequest = _mapper.Map<AddMandateRequest>(request);
                var url = await _gateway.RequestMandateAsync(netcashRequest, serviceKey);

                return url;
            }
            catch (NetcashGatewayException ex)
            {
                _logger.LogError(ex, "Mandate URL generation failed");
                throw;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex, "Mandate URL generation failed due to an unexpected parameter value.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while generating the mandate URL.");
                throw new Exception("An unexpected error occurred while generating the mandate URL.", ex);
            }
        }

        public async Task<BatchDebitOrderResponse> RequestBatchDebitOrderUpload(BatchDebitOrderRequest request, string serviceKey, string vendorKey) 
        {
            try
            {
                var response = await _gateway.GenerateDebitOrderFiles(request, serviceKey, vendorKey);
                return response;
            }
            catch (NetcashGatewayException ex)
            {
                _logger.LogError(ex, "Batch debit order upload failed");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while batch uploading debit orders");
                throw new Exception("An unexpected error occurred while batch uploading debit orders", ex);
            }
        }
    }
}
