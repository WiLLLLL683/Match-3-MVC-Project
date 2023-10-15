namespace Model.Readonly
{
    /// <summary>
    /// Сервис для работы с игровыми валютами и очками
    /// </summary>
    public interface ICurrencyService_Readonly
    {
        /// <summary>
        /// Получить количество валюты определенного типа в инвентаре
        /// </summary>
        int GetAmount(CurrencyType type);
    }
}