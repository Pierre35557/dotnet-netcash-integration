using AutoMapper;
using Microsoft.Extensions.Logging;
using Netcash.Common.Exceptions;
using Netcash.Data;
using Netcash.Data.Requests;
using Netcash.Domain.Interfaces;
using Netcash.DTO.Requests;

namespace Netcash.Domain.Services
{
    public class NetcashService : INetcashService
    {
        private readonly IMapper _mapper;
        public readonly INetcashGateway _gateway;
        public readonly ILogger<NetcashService> _logger;

        public NetcashService(IMapper mapper, INetcashGateway gateway, ILogger<NetcashService> logger)
        {
            _mapper = mapper;
            _gateway = gateway;
            _logger = logger;
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while generating the mandate URL");
                throw new Exception("An unexpected error occurred while generating the mandate URL", ex);
            }
        }
    }
}
