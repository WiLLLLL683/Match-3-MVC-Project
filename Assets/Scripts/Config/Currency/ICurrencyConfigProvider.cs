using Model.Objects;
using System.Collections.Generic;

namespace Config
{
    public interface ICurrencyConfigProvider
    {
        List<CurrencySO> GetAllSO();
        CurrencySO GetSO(CurrencyType type);
    }
}