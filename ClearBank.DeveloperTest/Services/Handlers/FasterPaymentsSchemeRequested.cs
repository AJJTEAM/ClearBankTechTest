using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Handlers
{
    public class FasterPaymentsSchemeRequested
    {
        public static MakePaymentResult Handle(Account account, decimal amount)
        {
            var result = new MakePaymentResult();
            if (IsValidFasterPayments(account, amount))
            {
                result.Success = true;
            }
            return result;
        }

        private static bool IsValidFasterPayments(Account account, decimal amount)
        => account != null &&
           account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) &&
           account.Balance > amount &&
           amount != 0;
    }
}
