using Model.Services;
using System.Collections.Generic;
using View;

namespace Config
{
    public interface IConfigProvider
    {
        TurnSO Turn { get; }
        BlockView BlockViewPrefab { get; }
        BlockTypeSO GetBlockTypeSO(int id);
        CellTypeSO HiddenCellType { get; }

        CellTypeSO GetCellTypeSO(int id);
        ACounterTargetSO GetCounterTargetSO(int id);
    }
}