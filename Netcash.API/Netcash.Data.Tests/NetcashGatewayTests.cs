using Microsoft.Extensions.Logging;
using Netcash.Data.Requests;
using NIWS;
using NSubstitute;

namespace Netcash.Data.Tests
{
    [TestFixture]
    public class NetcashGatewayTests
    {
        private INIWS_NIF _mockClient;
        private NetcashGateway _netcashGateway;
        private ILogger<NetcashGateway> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockClient = Substitute.For<INIWS_NIF>();
            _mockLogger = Substitute.For<ILogger<NetcashGateway>>();
            _netcashGateway = new NetcashGateway(_mockClient, _mockLogger);
        }

        [Test]
        public async Task RequestMandateAsync_ShouldReturnMandateUrl_WhenSuccessful()
        {
            //---------------Arrange-------------------
            var request = new AddMandateRequest
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
            string expectedMandateUrl = "https://short.surf/";

            var response = new AddMandateResponse { MandateUrl = expectedMandateUrl };

            _mockClient.AddMandateAsync(
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>(),
                Arg.Any<bool>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MandateOptionsMandateDebitFrequency>(),
                Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int?>(),
                Arg.Any<MandateOptionsMandatePublicHolidayOption?>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool?>(), Arg.Any<int?>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MandateOptionsBankAccountType?>(),
                Arg.Any<string>(), Arg.Any<int?>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool?>(),
                Arg.Any<MandateOptionsTitle?>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<bool?>(), Arg.Any<bool?>(), Arg.Any<string>(),
                Arg.Any<bool>(), Arg.Any<string>(), Arg.Any<decimal?>(), Arg.Any<bool?>(),
                Arg.Any<string>(), Arg.Any<decimal?>(), Arg.Any<DebiCheckOptionsCollectionFrequencyDayCodes?>(), Arg.Any<bool>()
            ).ReturnsForAnyArgs(Task.FromResult(response));

            //---------------Act-----------------------
            string result = await _netcashGateway.RequestMandateAsync(request, serviceKey);

            //---------------Assert--------------------
            Assert.That(expectedMandateUrl, Is.EqualTo(result));
        }

        //TODO: implement failure cases.

        //[Test]
        //public async Task RequestMandateAsync_ShouldThrowNetcashException_OnError()
        //{
        //    //---------------Arrange-------------------
        //    var request = new AddMandateRequest { AccountReference = "ACC123", MandateName = "TestMandate" };

        //    string serviceKey = "TEST_SERVICE_KEY";
        //    string expectedMandateUrl = "https://short.surf/";

        //    _mockClient.AddMandateAsync(
        //        Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>(),
        //        Arg.Any<bool>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
        //        Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MandateOptionsMandateDebitFrequency>(),
        //        Arg.Any<int>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int?>(),
        //        Arg.Any<MandateOptionsMandatePublicHolidayOption?>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
        //        Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
        //        Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool?>(), Arg.Any<int?>(),
        //        Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MandateOptionsBankAccountType?>(),
        //        Arg.Any<string>(), Arg.Any<int?>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool?>(),
        //        Arg.Any<MandateOptionsTitle?>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
        //        Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
        //        Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
        //        Arg.Any<string>(), Arg.Any<bool?>(), Arg.Any<bool?>(), Arg.Any<string>(),
        //        Arg.Any<bool>(), Arg.Any<string>(), Arg.Any<decimal?>(), Arg.Any<bool?>(),
        //        Arg.Any<string>(), Arg.Any<decimal?>(), Arg.Any<DebiCheckOptionsCollectionFrequencyDayCodes?>(), Arg.Any<bool>()
        //    ).ThrowsAsync(new Exception("Netcash API Error"));

        //    //---------------Act && Assert-----------------------
        //    string result = await _netcashGateway.RequestMandateAsync(request, serviceKey);
        //    Assert.That(expectedMandateUrl, Is.EqualTo(result));
        //}
    }
}
