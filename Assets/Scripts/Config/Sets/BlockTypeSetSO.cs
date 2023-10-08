﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "NewBlockTypeSet", menuName = "Config/BlockTypeSet")]
    public class BlockTypeSetSO: ScriptableObject
    {
        [Serializable]
        public class BlockTypeSO_Weight
        {
            public BlockTypeSO blockTypeSO;
            public int weight;
        }

        public List<BlockTypeSO_Weight> typeWeights = new();
        public BlockTypeSO defaultBlockType;

        public BlockTypeSO GetSO(int id)
        {
            for (int i = 0; i < typeWeights.Count; i++)
            {
                if (typeWeights[i].blockTypeSO.type.Id == id)
                    return typeWeights[i].blockTypeSO;
            }

            return defaultBlockType;
        }
    }
}