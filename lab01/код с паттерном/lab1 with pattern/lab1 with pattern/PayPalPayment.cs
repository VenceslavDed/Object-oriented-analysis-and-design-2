using System;
using System.Threading;

namespace PaymentApp
{
    public class PayPalPayment : IPaymentProcessor
    {
        public string Email { get; set; }

        public PayPalPayment(string email)
        {
            Email = email;
        }

        public bool Check()
        {
            return !string.IsNullOrWhiteSpace(Email)
                && Email.Contains("@")
                && Email.Contains(".");
        }

        public bool Process(decimal amount)
        {
            Thread.Sleep(2000);
            return new Random().Next(0, 10) != 0; // 90% успех
        }

        public string GetDisplayName() => "PayPal";
    }
}
