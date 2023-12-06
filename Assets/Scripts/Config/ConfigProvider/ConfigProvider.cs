using Model.Objects;
using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Config
{
    public class ConfigProvider : IConfigProvider
    {
        public BlockView BlockViewPrefab { get; }
        public CellTypeSO HiddenCellType => allCellTypes.hiddenCellType;
        public TurnSO Turn => allCounterTargets.turnSO;
        public int LastLevelIndex => allLevels.levels.Count - 1;

        private readonly BlockTypeSetSO allBlockTypes;
        private readonly CellTypeSetSO allCellTypes;
        private readonly CounterTargetSetSO allCounterTargets;
        private readonly CurrencySetSO allCurrencies;
        private readonly LevelSetSO allLevels;

        public ConfigProvider(BlockTypeSetSO allBlockTypes,
            BlockView blockViewPrefab,
            CellTypeSetSO allCellTypes,
            CounterTargetSetSO allCounterTargets,
            CurrencySetSO allCurrencies,
            LevelSetSO allLevels)
        {
            this.allBlockTypes = allBlockTypes;
            this.BlockViewPrefab = blockViewPrefab;
            this.allCellTypes = allCellTypes;
            this.allCounterTargets = allCounterTargets;
            this.allCurrencies = allCurrencies;
            this.allLevels = allLevels;
        }

        public BlockTypeSO GetBlockTypeSO(int id)
        {
            List<BlockTypeSO_Weight> types = allBlockTypes.typeWeights;

            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].blockTypeSO.type.Id == id)
                    return types[i].blockTypeSO;
            }

            return allBlockTypes.defaultBlockType;
        }

        public CellTypeSO GetCellTypeSO(int id)
        {
            List<CellTypeSO> types = allCellTypes.cellTypes;

            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].type.Id == id)
                    return types[i];
            }

            return allCellTypes.defaultCellType;
        }

        public ACounterTargetSO GetCounterTargetSO(int id)
        {
            List<ACounterTargetSO> targets = allCounterTargets.targets;

            for (int i = 0; i < targets.Count; i++)
            {
                if (id == targets[i].CounterTarget.Id)
                {
                    return targets[i];
                }
            }

            return allCounterTargets.defaultTarget;
        }

        public CurrencySO GetCurrencySO(CurrencyType type)
        {
            List<CurrencySO> currencies = allCurrencies.currencies;

            for (int i = 0; i < currencies.Count; i++)
            {
                if (currencies[i].type == type)
                    return currencies[i];
            }

            return null;
        }

        public List<CurrencySO> GetAllCurrenciesSO() => allCurrencies.currencies;

        public LevelSO GetLevelSO(int index)
        {
            List<LevelSO> levels = allLevels.levels;

            if (!levels.IsInBounds(index))
                return allLevels.defaultLevel;

            return levels[index];
        }
    }
}