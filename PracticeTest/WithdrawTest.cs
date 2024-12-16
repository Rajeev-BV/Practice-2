using Moq;
using Moq.Protected;
using  Practice_2;
using System.Net;

namespace PracticeTest

{
    public class Tests
    {
        Withdraw _withdraw;
        bool _eventRaised = false;
        object _eventSource;
        WithdrawalEventArgs _eventArgs;
        string message = null;
        private double taxAmount;

        [SetUp]
        public void Setup()
        {
          
        }

        [Test]
        public async Task WithdrawTest_WithdrawalAmount_LessThan_Limit_NoEventRaised()
        {
            //Arrange
            int amount = 4000;
            var mockBankRepo = new Mock<IBankRepository>();
            mockBankRepo.Setup(balance => balance.GetBalanceAmount("ABC")).ReturnsAsync(50000);
            Withdraw withdraw = new Withdraw(mockBankRepo.Object);
            withdraw.OnLimitExceeded += new WithdrawalLimitExceeded(withdraw_onlimtexceeded);
            //Act
            await withdraw.WithdrawMoney(amount);
            //Assert
            mockBankRepo.Verify(x => x.UpdateBalance("ABC", 46000), Times.Once);
            Assert.False(_eventRaised);
           
        }

        [Test]
        public async Task WithdrawTest_WithdrawalAmount_GreaterThan_Limit_Raise_LimitExceeded_Event()
        {
            //Arrange
            int amount = 46000;
            var mockBankRepo = new Mock<IBankRepository>();
            mockBankRepo.Setup(balance => balance.GetBalanceAmount("ABC")).ReturnsAsync(50000);
            Withdraw withdraw = new Withdraw(mockBankRepo.Object);
            withdraw.OnLimitExceeded += new WithdrawalLimitExceeded(withdraw_onlimtexceeded);
            //Act
            await withdraw.WithdrawMoney(amount);
            //Assert
            mockBankRepo.Verify(x => x.UpdateBalance("ABC", 4000), Times.Once);
            Assert.IsTrue(_eventRaised);
            Assert.AreEqual(message, "Lower limit rwched");
        }

        private void withdraw_onlimtexceeded(object source, WithdrawalEventArgs e)
        {
            _eventRaised = true;
            _eventSource = source;
            _eventArgs = e;
            message = e.nessage;
        }
        [TearDown]
        public void TearDown()
        {
            _eventRaised = false;
        }

       
        [TestCase(3, 4, 7)]
        [TestCase(0, 4, 4)]

        public void Test_Addition(int a, int b, int result)
        {
            Withdraw withdraw = new Withdraw(null);
            int addresult = withdraw.Add(a, b);

            Assert.AreEqual(result, addresult);

        }

        [Test]
        public async Task Test_TaxCalculatorAsync()
        {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(@"[{ ""SlabAmount"": 1000000, ""taxAmount"": 30}, { ""SlabAmount"": 500000, ""taxAmount"": 20}, { ""SlabAmount"": 300000, ""taxAmount"": 5}]"),
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var taxCalculator = new TaxCalculator(httpClient);
            //Act
            taxAmount = await taxCalculator.CalculateTaxAsync(600000);
            //Assert
            Assert.AreEqual(30000, taxAmount);
        }

    }
}