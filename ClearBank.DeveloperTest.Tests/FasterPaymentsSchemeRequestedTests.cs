using ClearBank.DeveloperTest.Services.Handlers;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
    public class FasterPaymentsSchemeRequestedTests
    {
        [Fact]
        public void Handle_Should_Return_False_When_Invalid_FasterPayments()
        {
            //Arrange
            var account = new Account { AccountNumber = "TestAccount123", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs };

            //Act
            MakePaymentResult result = FasterPaymentsSchemeRequested.Handle(account, 10);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Handle_Should_Return_True_When_Valid_FasterPayments()
        {
            //Arrange
            var account = new Account { AccountNumber = "TestAccount123", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 100};

            //Act
            MakePaymentResult result = FasterPaymentsSchemeRequested.Handle(account, 10);

            //Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void Handle_Should_Return_False_When_Insuffient_Balance()
        {
            //Arrange
            var account = new Account { AccountNumber = "TestAccount123", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 100 };

            //Act
            MakePaymentResult result = FasterPaymentsSchemeRequested.Handle(account, 1000);

            //Assert
            Assert.False(result.Success);
        }
    }
}
