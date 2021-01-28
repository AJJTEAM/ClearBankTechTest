using ClearBank.DeveloperTest.Types;
using System.Collections.Generic;

namespace ClearBank.DeveloperTest.Services.Handlers
{
    public static class PaymentSchemeHandlerDictionary
    {
        public static Dictionary<PaymentScheme, EventRaiser> EventHandlers()
        {
            return new Dictionary<PaymentScheme, EventRaiser>
            {
                { PaymentScheme.Bacs, BacsPaymentSchemeRequested.Handle},
                { PaymentScheme.Chaps, ChapsPaymentSchemeRequested.Handle },
                { PaymentScheme.FasterPayments, FasterPaymentsSchemeRequested.Handle }
            };
        }
    }
}
