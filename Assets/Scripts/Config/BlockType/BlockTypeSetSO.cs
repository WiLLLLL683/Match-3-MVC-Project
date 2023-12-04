using Model.Services;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using View;

namespace Config
{
    [CreateAssetMenu(fileName = "NewBlockTypeSet", menuName = "Config/BlockTypeSet")]
    public class BlockTypeSetSO : ScriptableObject
    {
        public List<BlockTypeSO_Weight> typeWeights = new();
        public BlockTypeSO defaultBlockType;

        public List<BlockType_Weight> GetWeights()
        {
            List<BlockType_Weight> weights = new();

            for (int i = 0; i < typeWeights.Count; i++)
            {
                weights.Add(new(typeWeights[i].blockTypeSO.type, typeWeights[i].weight));
            }

            return weights;
        }

#if UNITY_EDITOR
        private readonly AssetFinder assetFinder = new();
        private readonly UniqueIdChecker idChecker = new();

        [Button] public void CheckUniqueId() => idChecker.CheckBlockTypeWeight(typeWeights);
        [Button] public void FindAllBlockTypesInProject() => assetFinder.FindAllBlockTypes(ref typeWeights, this);
#endif
    }
}