using Microsoft.AspNetCore.Mvc;
using Netcash.Common.Exceptions;
using Netcash.Common.Models;
using Netcash.Domain.Interfaces;
using Netcash.DTO.Requests;
using Netcash.DTO.Responses;
using Newtonsoft.Json;
using System.ComponentModel;

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

        [Description("Initializes a new instance of the <see cref=\"NetcashController\"/>.")]
        public NetcashController(ILogger<NetcashController> logger, INetcashService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Initiates the eMandate creation process and returns a URL where the user can review and approve the mandate.
        /// 
        /// Notes:
        /// - An email containing the OTP and eMandate URL is sent to the provided email address upon initiation.
        /// - For additional security, the OTP required to sign the mandate is sent separately via SMS.
        /// - This ensures a two-step authentication process, enhancing mandate approval security.
        /// </summary>
        /// <param name="request">The mandate creation request details.</param>
        /// <param name="serviceKey">The Netcash service key from request headers.</param>
        /// <returns>A response containing the mandate URL.</returns>
        [HttpPost("mandate")]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [EndpointDescription(@"Initiates the eMandate creation process and returns a URL for mandate review and approval.

    Notes:
    - OTP and eMandate URL is sent to the provided email address upon initiation.
    - The OTP required to sign the mandate is sent separately via SMS.
    - This ensures a two-step authentication process, enhancing mandate approval security."
        )]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RequestMandateUrl([FromBody] CreateMandateRequest request,
            [FromHeader(Name = "X-Netcash-Service-Key")] string serviceKey) //TODO: encrypt key
        {
            if (string.IsNullOrWhiteSpace(serviceKey))
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Missing Netcash service key in request headers.",
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
                var mandateUrl = await _service.RequestMandateUrl(request, serviceKey);
                return Ok(new ApiResponse<string>(
                    true,
                    "Mandate succesfully created",
                    StatusCodes.Status201Created,
                    mandateUrl
                ));
            }
            catch (NetcashGatewayException ex)
            {
                _logger.LogError(ex, "Error processing RequestMandateUrl request.");
                return BadRequest(new ApiResponse<object>(
                    false,
                    $"An error occurred while processing your request: {ex.Message}",
                    StatusCodes.Status400BadRequest));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex, "Unexpected error processing RequestMandateUrl request.");
                return BadRequest(new ApiResponse<object>(
                    false,
                    $"An error occurred while processing your request: {ex.Message}",
                    StatusCodes.Status500InternalServerError));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in RequestMandateUrl.");
                return StatusCode(500, new ApiResponse<object>(
                    false,
                    $"An error occurred while processing your request: {ex.Message}",
                    StatusCodes.Status500InternalServerError));
            }
        }

        /// <summary>
        /// Initiates a batch debit order upload.
        /// </summary>
        /// <param name="request">The batch debit order request details.</param>
        /// <param name="serviceKey">The Netcash service key from request headers.</param>
        /// <param name="vendorKey">The Netcash vendor key from request headers.</param>
        /// <returns>A response indicating the outcome of the batch upload.</returns>
        [HttpPost("debit/batch")]
        [MapToApiVersion("1.0")]
        [Consumes("application/json")]
        [EndpointDescription("Initiates the batch debit order upload process. The response contains the file token and upload report.")]
        [ProducesResponseType(typeof(ApiResponse<BatchDebitOrderResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RequestBatchDebitOrderUpload([FromBody] BatchDebitOrderRequest request,
            [FromHeader(Name = "X-Netcash-Service-Key")] string serviceKey,
            [FromHeader(Name = "X-Netcash-Vendor-Key")] string vendorKey) //TODO: encrypt key
        {
            if (string.IsNullOrWhiteSpace(serviceKey))
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Missing Netcash service key in request headers.",
                    StatusCodes.Status400BadRequest)
                );
            }

            if (string.IsNullOrWhiteSpace(vendorKey))
            {
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Missing Netcash vendor key in request headers.",
                    StatusCodes.Status400BadRequest)
                );
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new ApiResponse<object>(
                    false,
                    "Invalid request",
                    StatusCodes.Status400BadRequest,
                    errors: errors
                ));
            }

            try
            {
                var reponse = await _service.RequestBatchDebitOrderUpload(request, serviceKey, vendorKey);
                return Ok(new ApiResponse<BatchDebitOrderResponse>(
                    true,
                    "Batch debit order uploaded successfully.",
                    StatusCodes.Status201Created,
                    reponse
                 ));
            }
            catch (NetcashGatewayException ex)
            {
                _logger.LogError(ex, "Error processing RequestBatchDebitOrderUpload request.");
                return BadRequest(new ApiResponse<object>(
                    false,
                    $"An error occurred while processing your request: {ex.Message}",
                    StatusCodes.Status400BadRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in RequestBatchDebitOrderUpload.");
                return StatusCode(500, new ApiResponse<object>(
                    false,
                    $"An error occurred while processing your request: {ex.Message}",
                    StatusCodes.Status500InternalServerError));
            }
        }

        /// <summary>
        /// <summary>
        /// Example endpoint demonstrating how mandate responses are received from Netcash.
        /// Although not used internally, this endpoint can be implemented for handling Netcash mandate webhooks if required.
        /// </summary>
        /// <returns>200 OK</returns>

        [HttpPost]
        [Route("callback")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ProcessMandateCallback([FromForm] NetcashMandateWebhookResponse response)
        {
            var serializedResponse = JsonConvert.SerializeObject(response, Formatting.Indented);
            Console.WriteLine(serializedResponse);

            return Ok();
        }
    }
}
