using System;

namespace Config
{
    [Serializable]
    public class AllSetsConfig
    {
        public LevelSetSO allLevels;
        public BlockTypeSetSO allBlockTypes;
        public CellTypeSetSO allCellTypes;
        public CurrencySetSO allCurrencies;
        public CounterTargetSetSO allCounterTargets;
        public BoosterSetSO allBoosters;
    }
}