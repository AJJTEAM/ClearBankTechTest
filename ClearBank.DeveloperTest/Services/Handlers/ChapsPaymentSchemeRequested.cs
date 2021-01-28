using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Handlers
{
    public class ChapsPaymentSchemeRequested
    {
        public static MakePaymentResult Handle(Account account, decimal amount = 0)
        {
            var result = new MakePaymentResult();
            if (IsValidChaps(account))
            {
                result.Success = true;
            }
            return result;
        }

        private static bool IsValidChaps(Account account)
        => account != null &&
           account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) &&
           account.Status == AccountStatus.Live; 
    }
}
