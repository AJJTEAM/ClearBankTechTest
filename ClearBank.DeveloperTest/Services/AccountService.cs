using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services.Configuration;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IBackupAccountDataStore _backupAccountDataStore;
        private readonly IAccountDataStore _accountDataStore;
        private readonly IConfigurationProvider _configurationProvider;

        public AccountService(IBackupAccountDataStore backupAccountDataStore,
            IAccountDataStore accountDataStore,
            IConfigurationProvider configurationProvider)
        {
            _backupAccountDataStore = backupAccountDataStore;
            _accountDataStore = accountDataStore;
            _configurationProvider = configurationProvider;
        }

        public Account GetAccount(string accountNumber)
            => _configurationProvider.DataStoreType == DataStoreTypeEnum.Backup.ToString()
                    ? _backupAccountDataStore.GetAccount(accountNumber)
                    : _accountDataStore.GetAccount(accountNumber);
        public void UpdateAccount(Account account)
        {
            if (_configurationProvider.DataStoreType == DataStoreTypeEnum.Backup.ToString())
                _backupAccountDataStore.UpdateAccount(account);
            else _accountDataStore.UpdateAccount(account);
        }
    }
}
