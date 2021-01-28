using System;

namespace ClearBank.DeveloperTest.Exceptions
{
    public class PaymentSchemeNotFoundException : Exception
    {
        public string PaymentScheme { get; }
        public string DebtorAccountNumber { get; }
        public string CreditorAccountNumber { get; }

        public PaymentSchemeNotFoundException(string paymentScheme, string debtorAccountNumber, string creditorAccountNumber)
        {
            PaymentScheme = paymentScheme;
            DebtorAccountNumber = debtorAccountNumber;
            CreditorAccountNumber = creditorAccountNumber;
        }
    }
}
