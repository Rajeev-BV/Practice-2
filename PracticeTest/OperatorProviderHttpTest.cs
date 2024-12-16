using Moq;
using Moq.Protected;
using Practice_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTest
{
    public  class OperatorProviderHttpTest
    {
        private const string targetURI = "https://localhost";
        [TestCase(@"[{ ""OperatorName"": ""+""}]", 9)]
        [TestCase(@"[{ ""OperatorName"": ""-""}]", 0)]
        public async Task Test_Get_Operator_DataAsync(string content, int expectedOperator)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content),
                //Content = new StringContent("Mocked response data"),
            };
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object)
            { BaseAddress = new Uri(targetURI) };

            var operatorProvider = new OperatorProvider(httpClient);
            int aritmeticOperator = await operatorProvider.GetOperatorAndAdd();

            Assert.AreEqual(expectedOperator, aritmeticOperator);
            handlerMock.Protected().Verify(
           "SendAsync",
           Times.Exactly(1),
           ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get
           && req.RequestUri.ToString() == targetURI + "/api/getOperator"),
           ItExpr.IsAny<CancellationToken>());

        }
    }
}
