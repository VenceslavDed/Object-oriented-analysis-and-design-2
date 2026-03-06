using System;
using System.Threading;

namespace PaymentApp
{
    public class CryptoPayment : IPaymentProcessor
    {
        public string WalletAddress { get; set; }
        public string Coin          { get; set; }

        public CryptoPayment(string walletAddress, string coin)
        {
            WalletAddress = walletAddress;
            Coin          = coin;
        }

        public bool Check()
        {
            if (string.IsNullOrWhiteSpace(WalletAddress)) return false;
            if (WalletAddress.Length < 26)                return false;
            if (string.IsNullOrWhiteSpace(Coin))          return false;
            return true;
        }

        public bool Process(decimal amount)
        {
            Thread.Sleep(3000);
            return new Random().Next(0, 7) != 0; // ~85% успех
        }

        public string GetDisplayName() => $"Криптовалюта ({Coin})";
    }
}
