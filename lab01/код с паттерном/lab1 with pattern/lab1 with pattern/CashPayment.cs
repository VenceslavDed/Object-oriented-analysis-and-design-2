using System;
using System.Threading;

namespace PaymentApp
{
    public class CashPayment : IPaymentProcessor
    {
        public string Address { get; set; }
        public string Phone   { get; set; }

        public CashPayment(string address, string phone)
        {
            Address = address;
            Phone   = phone;
        }

        public bool Check()
        {
            if (string.IsNullOrWhiteSpace(Address)) return false;
            if (string.IsNullOrWhiteSpace(Phone))   return false;
            return true;
        }

        public bool Process(decimal amount)
        {
            Thread.Sleep(800);
            return true; // наложенный платёж всегда успешно регистрируется
        }

        public string GetDisplayName() => "Наличными при получении";
    }
}
