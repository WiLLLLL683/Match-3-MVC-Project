using Model.Objects;
using Model.Services;
using System.Collections.Generic;
using View;

namespace Config
{
    public interface IConfigProvider
    {
        BoosterSO GetBoosterSO(int id);

        PrefabConfig Prefabs { get; }
        DelayConfig Delays { get; }
        CellTypeSO HiddenCellType { get; }
        TurnSO Turn { get; }
        int LastLevelIndex { get; }

        BlockTypeSO GetBlockTypeSO(int id);
        CellTypeSO GetCellTypeSO(int id);
        ACounterTargetSO GetCounterTargetSO(int id);
        CurrencySO GetCurrencySO(CurrencyType type);
        List<CurrencySO> GetAllCurrenciesSO();
        LevelSO GetLevelSO(int index);
    }
}