using Model.Objects;
using System;

namespace Model.Services
{
    /// <summary>
    /// Сервис для работы с игровыми валютами и очками
    /// </summary>
    public interface ICurrencyService
    {
        event Action<CurrencyType, int> OnChange;

        void ClearAllCurrencies();

        /// <summary>
        /// Добавить валюту определенного типа
        /// </summary>
        void AddCurrency(CurrencyType type, int ammount);

        /// <summary>
        /// Потратить валюту определенного типа из инвентаря
        /// </summary>
        void SpendCurrency(CurrencyType type, int ammount);

        /// <summary>
        /// Получить количество валюты определенного типа в инвентаре
        /// </summary>
        int GetAmount(CurrencyType type);
    }
}