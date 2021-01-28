using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private IPaymentService _sut;

        public PaymentServiceTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _sut = new PaymentService(_accountServiceMock.Object);
        }

        [Fact]
        public void MakePayment_Should_Return_False_When_Account_Does_Not_Exist()
        {
            // Arrange
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<string>()));

            // Act
            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "TestAccountNumber123"});

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void MakePayment_Should_Return_True_When_Valid_Bacs_And_Account_Exists()
        {
            // Arrange
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(new Account { AccountNumber = "TestAccountNumber123", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs});

            // Act
            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "TestAccountNumber123", PaymentScheme = PaymentScheme.Bacs });

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void MakePayment_Should_Return_True_When_Valid_Chaps_And_Account_Exists()
        {
            // Arrange
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(new Account { AccountNumber = "TestAccountNumber123", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps });

            // Act
            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "TestAccountNumber123", PaymentScheme = PaymentScheme.Chaps });

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void MakePayment_Should_Return_True_When_Valid_Faster_And_Account_Exists()
        {
            // Arrange
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(new Account { AccountNumber = "TestAccountNumber123", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 100 });

            // Act
            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "TestAccountNumber123", PaymentScheme = PaymentScheme.FasterPayments, Amount = 10 });

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void MakePayment_Should_Update_Balance_When_Valid_Faster_And_Account_Exists()
        {
            var account = new Account { AccountNumber = "TestAccountNumber123", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 100 };
            // Arrange
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);
            _accountServiceMock.Setup(x => x.UpdateAccount(It.IsAny<Account>())).Verifiable();
            // Act
            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "TestAccountNumber123", PaymentScheme = PaymentScheme.FasterPayments, Amount = 50 });

            // Assert
            Assert.Equal(50,account.Balance);
            _accountServiceMock.Verify(_ => _.UpdateAccount(It.IsAny<Account>()), Times.Once);
        }

        [Fact]
        public void MakePayment_Should_Update_Balance_When_Valid_Bacs_And_Account_Exists()
        {
            var account = new Account { AccountNumber = "TestAccountNumber123", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 100 };
            // Arrange
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);
            _accountServiceMock.Setup(x => x.UpdateAccount(It.IsAny<Account>())).Verifiable();
            // Act
            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "TestAccountNumber123", PaymentScheme = PaymentScheme.Bacs, Amount = 50 });

            // Assert
            Assert.Equal(50, account.Balance);
            _accountServiceMock.Verify(_ => _.UpdateAccount(It.IsAny<Account>()), Times.Once);
        }

        [Fact]
        public void MakePayment_Should_Update_Balance_When_Valid_Chaps_And_Account_Exists()
        {
            var account = new Account { AccountNumber = "TestAccountNumber123", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Balance = 100 };
            // Arrange
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);
            _accountServiceMock.Setup(x => x.UpdateAccount(It.IsAny<Account>())).Verifiable();
            // Act
            var result = _sut.MakePayment(new MakePaymentRequest { DebtorAccountNumber = "TestAccountNumber123", PaymentScheme = PaymentScheme.Chaps, Amount = 50 });

            // Assert
            Assert.Equal(50, account.Balance);
            _accountServiceMock.Verify(_ => _.UpdateAccount(It.IsAny<Account>()), Times.Once);
        }
    }
}
