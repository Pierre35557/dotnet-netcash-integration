using Netcash.DTO.Requests;
using Netcash.DTO.Responses;

namespace Netcash.Domain.Interfaces
{
    public interface INetcashService
    {
        Task<string> RequestMandateUrl(CreateMandateRequest request, string serviceKey);
        Task<BatchDebitOrderResponse> RequestBatchDebitOrderUpload(BatchDebitOrderRequest request, string serviceKey, string vendorKey);
    }
}
