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

        private readonly BlockTypeSetSO allBlockTypes;

        public ConfigProvider(BlockTypeSetSO allBlockTypes, BlockView blockViewPrefab)
        {
            this.allBlockTypes = allBlockTypes;
            this.BlockViewPrefab = blockViewPrefab;
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
    }
}