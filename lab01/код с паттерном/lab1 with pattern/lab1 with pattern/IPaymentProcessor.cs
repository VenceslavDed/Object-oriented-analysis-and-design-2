namespace PaymentApp
{
    /// <summary>
    /// Контракт для всех способов оплаты.
    /// Любой класс реализующий этот интерфейс
    /// обязан реализовать все три метода.
    /// </summary>
    public interface IPaymentProcessor
    {
        /// <summary>Проверяет корректность введённых данных</summary>
        bool Check();

        /// <summary>Выполняет обработку платежа</summary>
        bool Process(decimal amount);

        /// <summary>Возвращает название способа оплаты для отображения в UI</summary>
        string GetDisplayName();
    }
}
