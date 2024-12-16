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
    public class PostTest
    {
        private const string targetURI = "https://localhost";
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task TestShouldReturnPostsAsync()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!""}, { ""id"": 100, ""title"": ""Some title""}]"),
            };
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object)
            { BaseAddress= new Uri(targetURI) };
            var posts = new Posts(httpClient);
            var retrievedPosts = await posts.GetPosts();
            int postID = await posts.GetPosts2();

            Assert.NotNull(retrievedPosts);
            Assert.AreEqual(2, postID);
         
            handlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get
            && req.RequestUri.ToString() == targetURI + "/api/posts"),
            ItExpr.IsAny<CancellationToken>());
        }
    }
}
