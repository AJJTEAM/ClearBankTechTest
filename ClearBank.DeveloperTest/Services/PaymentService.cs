using ClearBank.DeveloperTest.Exceptions;
using ClearBank.DeveloperTest.Services.Handlers;
using ClearBank.DeveloperTest.Types;
using System.Collections.Generic;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private static readonly IReadOnlyDictionary<PaymentScheme, EventRaiser> EventHandlers = PaymentSchemeHandlerDictionary.EventHandlers();
        private readonly IAccountService _accountService;

        public PaymentService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            Account account = _accountService.GetAccount(request.DebtorAccountNumber);
            MakePaymentResult result = Process(account, request);
            
            if (result.Success)
            {
                account.DeductFromBalance(request.Amount);
                _accountService.UpdateAccount(account);
            }

            return result;
        }

        private static MakePaymentResult Process(Account account, MakePaymentRequest request)
        {
            if (!EventHandlers.TryGetValue(request.PaymentScheme, out var eventHandler))
            {
                throw new PaymentSchemeNotFoundException(request.PaymentScheme.ToString(), request.DebtorAccountNumber, request.CreditorAccountNumber);
            }
            return eventHandler(account, request.Amount); 
        }
    }
}
