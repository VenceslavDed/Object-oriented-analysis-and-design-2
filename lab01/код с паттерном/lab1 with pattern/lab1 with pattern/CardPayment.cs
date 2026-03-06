using System;
using System.Threading;

namespace PaymentApp
{
    public class CardPayment : IPaymentProcessor
    {
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string Expiry { get; set; }

        public CardPayment(string cardNumber, string cvv, string expiry)
        {
            CardNumber = cardNumber;
            CVV = cvv;
            Expiry = expiry;
        }

        public bool Check()
        {
            if (CardNumber.Replace(" ", "").Length != 16) return false;
            if (CVV.Length != 3) return false;
            if (string.IsNullOrWhiteSpace(Expiry)) return false;
            return true;
        }

        public bool Process(decimal amount)
        {
            Thread.Sleep(1500);
            return new Random().Next(0, 5) != 0;
        }

        public string GetDisplayName() => "Банковская карта";
    }
}