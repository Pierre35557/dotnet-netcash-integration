using Netcash.Data.Requests;
using Netcash.DTO.Requests;
using Netcash.DTO.Responses;

namespace Netcash.Data
{
    public interface INetcashGateway
    {
        Task<string> RequestMandateAsync(AddMandateRequest request, string serviceKey);
        Task<BatchDebitOrderResponse> GenerateDebitOrderFiles(BatchDebitOrderRequest request, string serviceKey, string vendorKey);
        Task<BatchDebitOrderResponse> FetchBatchFileUploadReport(string serviceKey, string fileToken);
    }
}
