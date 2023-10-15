using Model.Readonly;

namespace Model.Services
{
    /// <summary>
    /// Сервис для работы с игровыми валютами и очками
    /// </summary>
    public interface ICurrencyService : ICurrencyService_Readonly
    {
        /// <summary>
        /// Добавить валюту определенного типа
        /// </summary>
        void AddCurrency(CurrencyType type, int ammount);

        /// <summary>
        /// Потратить валюту определенного типа из инвентаря
        /// </summary>
        void SpendCurrency(CurrencyType type, int ammount);
    }
}