using Model.Objects;
using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;
using Utils;

namespace Config
{
    public class ConfigProvider : IConfigProvider
    {
        public CellTypeSO HiddenCellType => allSets.allCellTypes.hiddenCellType;
        public TurnSO Turn => allSets.allCounterTargets.turnSO;
        public int LastLevelIndex => allSets.allLevels.levels.Count - 1;
        public DelayConfig Delays { get; }
        public PrefabConfig Prefabs { get; }
        public DefaultsConfig Defaults { get; }

        private readonly AllSetsConfig allSets;

        public ConfigProvider(AllSetsConfig allSets,
            DelayConfig delays,
            PrefabConfig prefabs,
            DefaultsConfig defaults)
        {
            this.allSets = allSets;
            this.Delays = delays;
            this.Prefabs = prefabs;
            this.Defaults = defaults;
        }

        public BlockTypeSO GetBlockTypeSO(int id)
        {
            List<BlockTypeSO_Weight> types = allSets.allBlockTypes.typeWeights;

            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].blockTypeSO.type.Id == id)
                    return types[i].blockTypeSO;
            }

            return allSets.allBlockTypes.defaultBlockType;
        }

        public CellTypeSO GetCellTypeSO(int id)
        {
            List<CellTypeSO> types = allSets.allCellTypes.cellTypes;

            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].type.Id == id)
                    return types[i];
            }

            return allSets.allCellTypes.defaultCellType;
        }

        public ACounterTargetSO GetCounterTargetSO(int id)
        {
            List<ACounterTargetSO> targets = allSets.allCounterTargets.targets;

            for (int i = 0; i < targets.Count; i++)
            {
                if (id == targets[i].CounterTarget.Id)
                {
                    return targets[i];
                }
            }

            return allSets.allCounterTargets.defaultTarget;
        }

        public CurrencySO GetCurrencySO(CurrencyType type)
        {
            List<CurrencySO> currencies = allSets.allCurrencies.currencies;

            for (int i = 0; i < currencies.Count; i++)
            {
                if (currencies[i].type == type)
                    return currencies[i];
            }

            return null;
        }

        public List<CurrencySO> GetAllCurrenciesSO() => allSets.allCurrencies.currencies;

        public LevelSO GetLevelSO(int index)
        {
            List<LevelSO> levels = allSets.allLevels.levels;

            if (!levels.IsInBounds(index))
                return allSets.allLevels.defaultLevel;

            return levels[index];
        }

        public BoosterSO GetBoosterSO(int id)
        {
            List<BoosterSO> boosters = allSets.allBoosters.boosters;

            for (int i = 0; i < boosters.Count; i++)
            {
                if (id == boosters[i].booster.Id)
                {
                    return boosters[i];
                }
            }

            return allSets.allBoosters.defaultBooster;
        }
    }
}