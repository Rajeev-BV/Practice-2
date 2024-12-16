using Moq.Protected;
using Moq;
using Practice_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTest
{
    public class TrafficViolationTest
    {
        private const string targetURI = "https://localhost";

        [TestCaseSource("FakeGetVehicleSpeedInALocation")]
        public async Task Given_Vehicle_Is_Within_Speed_Limit_Then_No_Fine_Should_Be_LeviedAsync(List<VehicleSpeedDetails> vehicleSpeedDetails)
        {
            //Arrange
            List<string> notifyViolationsExpcted = new List<string>();
            notifyViolationsExpcted.Add("KA90872");
            notifyViolationsExpcted.Add("KA9087");
            notifyViolationsExpcted = notifyViolationsExpcted.Where(item => item != null).ToList();
            int placeID = 1;

            Mock<HttpMessageHandler> handlerMock = MockHttpHandler();
            HttpClient httpClient = SetHttpClient(handlerMock);

            //Mock<IVehicleSpeedDetailsProvider> vehicleSpeedDetailsProviderMock = MockVehicleSpeedDetailsFromDB(placeID);
            var vehicleSpeedDetailsProviderMock = new Mock<IVehicleSpeedDetailsProvider>();
            vehicleSpeedDetailsProviderMock.Setup(vehicleSpeedDetails => vehicleSpeedDetails.GetVehicleSpeedInALocation(placeID)).Returns(vehicleSpeedDetails);

            //Act
            TrafficViolationNotifier trafficViolationNotifier = new TrafficViolationNotifier(httpClient, vehicleSpeedDetailsProviderMock.Object);
            List<string> notifyViolations = await trafficViolationNotifier.NotifyViolations(placeID);

            //Assert
            CollectionAssert.AreEqual(notifyViolationsExpcted, notifyViolations);
            handlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get
            && req.RequestUri.ToString() == targetURI + "/api/getSpeedLimits"),
            ItExpr.IsAny<CancellationToken>());

        }

        private static HttpClient SetHttpClient(Mock<HttpMessageHandler> handlerMock)
        {
            return new HttpClient(handlerMock.Object)
            { BaseAddress = new Uri(targetURI) };
        }

        private static Mock<HttpMessageHandler> MockHttpHandler()
        {
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""placeId"": 1, ""vehicleType"": ""bike"", ""speedLimit"" : 60}, 
                    { ""placeId"": 1, ""vehicleType"": ""car"",  ""speedLimit"" : 80}]"),
            };
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            return handlerMock;
        }

        private List<VehicleSpeedDetails> FakeGetVehicleSpeedInALocation_Case1()
        {

            List<VehicleSpeedDetails> vehicleSpeedDetailsList = new List<VehicleSpeedDetails> { new VehicleSpeedDetails("KA9087", 500, 1, "car"), new VehicleSpeedDetails("KA9087", 60, 1, "car") };

            return vehicleSpeedDetailsList;
        }

        private static object[] FakeGetVehicleSpeedInALocation = 
        {

            new object[] { new List<VehicleSpeedDetails> { new VehicleSpeedDetails("KA90872", 500, 1, "car"), new VehicleSpeedDetails("KA9087", 500, 1, "bike") } },
           //new object[] { new List<VehicleSpeedDetails> { new VehicleSpeedDetails("KA9087", 500, 1, "car") } },
         };

           
        }

    }

    


