using Model.Objects;
using Model.Services;
using System.Collections.Generic;
using View;

namespace Config
{
    public interface IConfigProvider
    {
        List<CurrencySO> GetAllCurrenciesSO();
        CurrencySO GetCurrencySO(CurrencyType type);

        TurnSO Turn { get; }
        BlockView BlockViewPrefab { get; }
        BlockTypeSO GetBlockTypeSO(int id);
        CellTypeSO HiddenCellType { get; }

        CellTypeSO GetCellTypeSO(int id);
        ACounterTargetSO GetCounterTargetSO(int id);
    }
}