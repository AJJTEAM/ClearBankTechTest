using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Configuration;
using ClearBank.DeveloperTest.Types;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests
{
    public class AccountServiceTests
    {
        private readonly Mock<IConfigurationProvider> _configurationProviderMock;
        private readonly Mock<IAccountDataStore> _accountDataStoreMock;
        private readonly Mock<IBackupAccountDataStore> _backupAccountDataStoreMock;
        private IAccountService _sut;

        public AccountServiceTests()
        {
            _configurationProviderMock = new Mock<IConfigurationProvider>();
            _accountDataStoreMock = new Mock<IAccountDataStore>();
            _backupAccountDataStoreMock = new Mock<IBackupAccountDataStore>();
            _sut = new AccountService(_backupAccountDataStoreMock.Object, _accountDataStoreMock.Object, _configurationProviderMock.Object);
        }

        [Fact]
        public void GetAccount_Should_Return_Account()
        {
            // Arrange
            _accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(new Account());

            // Act
            var account = _sut.GetAccount("TestAcount123");

            // Assert
            Assert.NotNull(account);
        }

        [Fact]
        public void GetAccount_Should_Return_Account_From_Backup()
        {
            // Arrange
            var accountBackup = new Account() { AccountNumber = "TestAccount123"};
            _configurationProviderMock.Setup(x => x.DataStoreType).Returns(DataStoreTypeEnum.Backup.ToString());
            _backupAccountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(accountBackup);

            // Act
            var account = _sut.GetAccount(It.IsAny<string>());

            // Assert
            Assert.Equal(account.AccountNumber, accountBackup.AccountNumber);
        }

        [Fact]
        public void GetAccount_Should_Return_Null_When_Account_Does_Not_Exist()
        {
            // Arrange
            _accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>()));

            // Act
            var account = _sut.GetAccount(It.IsAny<string>());

            // Assert
            Assert.Null(account);
        }

        [Fact]
        public void UpdateAccount_Should_UpdateAccountData()
        {
            // Arrange
            _accountDataStoreMock.Setup(x => x.UpdateAccount(It.IsAny<Account>())).Verifiable();

            // Act
            _sut.UpdateAccount(It.IsAny<Account>());

            // Assert
            _accountDataStoreMock.Verify(_ => _.UpdateAccount(It.IsAny<Account>()), Times.Once);
        }


        [Fact]
        public void UpdateAccount_Should_Update_BackupAccountData()
        {
            // Arrange
            _configurationProviderMock.Setup(x => x.DataStoreType).Returns(DataStoreTypeEnum.Backup.ToString());
            _backupAccountDataStoreMock.Setup(x => x.UpdateAccount(It.IsAny<Account>())).Verifiable(); 

            // Act
            _sut.UpdateAccount(It.IsAny<Account>());

            // Assert
            _backupAccountDataStoreMock.Verify(_ => _.UpdateAccount(It.IsAny<Account>()), Times.Once);
        }
    }
}
