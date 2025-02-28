using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Netcash.API.Controllers;
using Netcash.Common.Models;
using Netcash.Domain.Interfaces;
using Netcash.DTO.Requests;
using NSubstitute;

namespace Netcash.API.Tests
{
    [TestFixture]
    public class NetcashControllerTests
    {
        private INetcashService _mockService;
        private ILogger<NetcashController> _mockLogger;
        private NetcashController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = Substitute.For<INetcashService>();
            _mockLogger = Substitute.For<ILogger<NetcashController>>();
            _controller = new NetcashController(_mockLogger, _mockService);
        }

        [Test]
        public async Task RequestMandateUrl_ReturnsOkResult_WhenSuccessful()
        {
            //---------------Arrange-------------------
            var request = new CreateMandateRequest
            {
                // Populate required properties as needed for a valid request
            };

            string serviceKey = "TEST_SERVICE_KEY";
            string expectedMandateUrl = "https://test.netcash/mandate/xyz";

            _mockService.RequestMandateUrl(request, serviceKey)
                .Returns(Task.FromResult(expectedMandateUrl));

            //---------------Act-----------------------
            var result = await _controller.RequestMandateUrl(request, serviceKey);

            //---------------Assert--------------------
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Expected an OkObjectResult");

            var apiResponse = okResult.Value as ApiResponse<string>;
            Assert.That(apiResponse, Is.Not.Null, "Expected the result value to be an ApiResponse<string>");
            Assert.That(apiResponse.Success, Is.True);
            Assert.That(apiResponse.Message, Is.EqualTo("Mandate URL received"));
            Assert.That(apiResponse.Data, Is.EqualTo(expectedMandateUrl));
        }

        [Test]
        public async Task RequestMandateUrl_ReturnsBadRequest_WhenServiceKeyIsMissing()
        {
            //---------------Arrange-------------------
            var request = new CreateMandateRequest
            {
                AccountReference = "ACC123",
                MandateName = "TestMandate",
                MandateAmount = 100.00m,
                IsConsumer = true,
                FirstName = "John",
                Surname = "Doe",
                MobileNumber = "0712345678",
                AgreementDate = "20250228",
                AgreementReferenceNumber = "AGREE123"
            };

            string serviceKey = "";

            //---------------Act-----------------------
            var result = await _controller.RequestMandateUrl(request, serviceKey);

            //---------------Assert--------------------
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Expected a BadRequestObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));

            var apiResponse = badRequestResult.Value as ApiResponse<object>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.False);
            Assert.That(apiResponse.Message, Is.EqualTo("Missing Netcash service key in request headers"));
        }


        [Test]
        public async Task RequestMandateUrl_ReturnsBadRequest_WhenRequestIsNull()
        {
            //---------------Arrange-------------------
            CreateMandateRequest request = null;
            string serviceKey = "TEST_SERVICE_KEY";

            //---------------Act-----------------------
            var result = await _controller.RequestMandateUrl(request, serviceKey);

            //---------------Assert--------------------
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Expected a BadRequestObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));

            var apiResponse = badRequestResult.Value as ApiResponse<object>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.False);
            Assert.That(apiResponse.Message, Is.EqualTo("Invalid data"));
        }

        [Test]
        public async Task RequestMandateUrl_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            //---------------Arrange-------------------
            var request = new CreateMandateRequest
            {
                AccountReference = "ACC123",
                MandateName = "TestMandate",
                MandateAmount = 100.00m,
                IsConsumer = true,
                FirstName = "John",
                Surname = "Doe",
                MobileNumber = "0712345678",
                AgreementDate = "20250228",
                AgreementReferenceNumber = "AGREE123"
            };

            string serviceKey = "TEST_SERVICE_KEY";

            //TODO: improve error message
            _controller.ModelState.AddModelError("TestError", "Test error message");

            //---------------Act-----------------------
            var result = await _controller.RequestMandateUrl(request, serviceKey);

            //---------------Assert--------------------
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Expected a BadRequestObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));

            var apiResponse = badRequestResult.Value as ApiResponse<object>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.False);
            Assert.That(apiResponse.Message, Is.EqualTo("Invalid request"));
            Assert.That(apiResponse.Errors, Does.Contain("Test error message"));
        }

        [Test]
        public async Task RequestMandateUrl_ReturnsInternalServerError_WhenServiceThrowsException()
        {
            //---------------Arrange-------------------
            var request = new CreateMandateRequest
            {
                AccountReference = "ACC123",
                MandateName = "TestMandate",
                MandateAmount = 100.00m,
                IsConsumer = true,
                FirstName = "John",
                Surname = "Doe",
                MobileNumber = "0712345678",
                AgreementDate = "20250228",
                AgreementReferenceNumber = "AGREE123"
            };
            string serviceKey = "TEST_SERVICE_KEY";

            _mockService.RequestMandateUrl(request, serviceKey)
                .Returns<string>(x => { throw new Exception("Service failure"); });

            //---------------Act-----------------------
            var result = await _controller.RequestMandateUrl(request, serviceKey);

            //---------------Assert--------------------
            var objectResult = result as ObjectResult;
            Assert.That(objectResult, Is.Not.Null, "Expected an ObjectResult");
            Assert.That(objectResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));

            var apiResponse = objectResult.Value as ApiResponse<object>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.False);
            Assert.That(apiResponse.Message, Does.Contain("Service failure"));
        }
    }
}