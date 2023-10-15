namespace Model.Objects
{
    /// <summary>
    /// Инвентарь для игровых валют
    /// </summary>
    public interface ICurrencyInventory
    {
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