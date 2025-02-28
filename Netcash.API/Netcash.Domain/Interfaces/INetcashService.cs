using Netcash.DTO.Requests;

namespace Netcash.Domain.Interfaces
{
    public interface INetcashService
    {
        Task<string> RequestMandateUrl(CreateMandateRequest request, string serviceKey);
    }
}
