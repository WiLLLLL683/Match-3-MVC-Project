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

        private readonly BlockTypeSetSO allBlockTypes;
        private readonly CellTypeSetSO allCellTypes;

        public ConfigProvider(BlockTypeSetSO allBlockTypes, BlockView blockViewPrefab, CellTypeSetSO allCellTypes)
        {
            this.allBlockTypes = allBlockTypes;
            this.BlockViewPrefab = blockViewPrefab;
            this.allCellTypes = allCellTypes;
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
            var types = allCellTypes.cellTypes;

            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].type.Id == id)
                    return types[i];
            }

            return allCellTypes.defaultCellType;
        }
    }
}