using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Handlers
{
    public class BacsPaymentSchemeRequested
    {
        public static MakePaymentResult Handle(Account account, decimal amount = 0)
        {
            var result = new MakePaymentResult();
            if (IsValidBacs(account))
            {
                result.Success = true;
            }
            return result;
        }

        private static bool IsValidBacs(Account account)
        => account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
    }
}
