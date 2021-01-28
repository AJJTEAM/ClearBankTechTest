using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.Handlers
{
    public delegate MakePaymentResult EventRaiser(Account account, decimal amount = 0);
}
