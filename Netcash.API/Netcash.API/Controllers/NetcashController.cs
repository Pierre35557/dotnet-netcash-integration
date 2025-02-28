using Microsoft.AspNetCore.Mvc;
using Netcash.Common.Models;
using Netcash.Domain.Interfaces;
using Netcash.DTO.Requests;

namespace Netcash.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling Netcash eMandate requests.
    /// This API allows clients to initiate eMandates for debit orders.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/netcash")]
    [ApiVersion("1.0")]
    public class NetcashController : ControllerBase
    {
        private readonly ILogger<NetcashController> _logger;
        private readonly INetcashService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetcashController"/>.
        /// </summary>
        /// <param name="logger">Logger instance for tracking API operations.</param>
        /// <param name="service">Service handling Netcash API logic.</param>
        public NetcashController(ILogger<NetcashController> logger, INetcashService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Requests a mandate URL from Netcash.
        /// This endpoint initiates an eMandate process and returns a URL where the user can approve the mandate.
        /// </summary>
        /// <param name="request">The mandate request details.</param>
        /// <param name="netcashServiceKey">The Netcash service key, required for authentication.</param>
        /// <returns>
        /// A response containing the mandate URL if successful, or an error message otherwise.
        /// </returns>
        [HttpPost("mandate")]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RequestMandateUrl([FromBody] CreateMandateRequest request, 
            [FromHeader(Name = "X-Netcash-Service-Key")] string netcashServiceKey) //TODO: encrypt key
        {
            if (string.IsNullOrWhiteSpace(netcashServiceKey))
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Missing Netcash service key in request headers",
                    StatusCodes.Status400BadRequest)
                );
            }

            if (request == null)
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Invalid data",
                    StatusCodes.Status400BadRequest)
                );
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new ApiResponse<object>(
                    false, 
                    "Invalid request",
                    StatusCodes.Status400BadRequest, errors: errors)
                );
            }

            try
            {
                var mandateUrl = await _service.RequestMandateUrl(request, netcashServiceKey);
                return Ok(new ApiResponse<string>(
                    true, 
                    "Mandate URL received", 
                    StatusCodes.Status201Created, mandateUrl)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing request");
                return StatusCode(500, new ApiResponse<object>(
                    false,
                    $"An error occurred while processing your request: {ex.Message}",
                    StatusCodes.Status500InternalServerError)
                );
            }
        }
    }
}
