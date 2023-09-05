using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Balance", menuName = "Config/Balance")]
    public class BalanceSO: ScriptableObject
    {
        [Serializable]
        public class BlockType_Weight
        {
            public BlockTypeSO blockTypeSO;
            public int weight;
        }

        public List<BlockType_Weight> typeWeights = new();
        public BlockTypeSO defaultBlockType;
    }
}