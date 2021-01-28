using ClearBank.DeveloperTest.Services.Handlers;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
    public class ChapsPaymentSchemeRequestedTests
    {
        [Fact]
        public void Handle_Should_Return_False_When_Invalid_Chaps()
        {
            //Arrange
            var account = new Account { AccountNumber = "TestAccount123", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs };

            //Act
            MakePaymentResult result = ChapsPaymentSchemeRequested.Handle(account);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Handle_Should_Return_True_When_Valid_Chaps()
        {
            //Arrange
            var account = new Account { AccountNumber = "TestAccount123", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps };

            //Act
            MakePaymentResult result = ChapsPaymentSchemeRequested.Handle(account);

            //Assert
            Assert.True(result.Success);
        }
    }
}
