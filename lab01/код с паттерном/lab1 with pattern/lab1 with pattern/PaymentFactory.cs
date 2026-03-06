using System;

namespace PaymentApp
{
    /// <summary>
    /// Фабрика способов оплаты.
    /// Единственное место в проекте где знают о конкретных классах.
    /// Form работает только через IPaymentProcessor.
    /// </summary>
    public class PaymentFactory
    {
        public IPaymentProcessor Create(string type, PaymentData data)
        {
            switch (type)
            {
                case "card":
                    return new CardPayment(data.CardNumber, data.CVV, data.Expiry);
                case "paypal":
                    return new PayPalPayment(data.Email);
                case "crypto":
                    return new CryptoPayment(data.WalletAddress, data.Coin);
                case "cash":
                    return new CashPayment(data.Address, data.Phone);
                default:
                    throw new ArgumentException($"Неизвестный тип оплаты: {type}");
            }
        }
    }

    /// <summary>
    /// Хранит все возможные поля формы.
    /// Фабрика берёт из него только те поля которые нужны конкретному типу.
    /// </summary>
    public class PaymentData
    {
        public string CardNumber    { get; set; }
        public string CVV           { get; set; }
        public string Expiry        { get; set; }
        public string Email         { get; set; }
        public string WalletAddress { get; set; }
        public string Coin          { get; set; }
        public string Address       { get; set; }
        public string Phone         { get; set; }
    }
}
