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

        private readonly Dictionary<int, BlockTypeSO> idTypePairs = new();

        private void OnValidate()
        {
            idTypePairs.Clear();
            for (int i = 0; i < typeWeights.Count; i++)
            {
                idTypePairs.Add(typeWeights[i].blockTypeSO.blockType.Id, typeWeights[i].blockTypeSO);
            }
        }

        public BlockTypeSO GetSO(int id)
        {
            if (!idTypePairs.ContainsKey(id))
                return defaultBlockType;

            return idTypePairs[id];
        }
    }
}