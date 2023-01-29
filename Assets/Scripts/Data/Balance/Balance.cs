using Model.Objects;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{

    [CreateAssetMenu(fileName = "Balance", menuName = "Data/Balance")]
    public class Balance: ScriptableObject
    {
        [SerializeField] private List<BlockType_Weight> typesWeight;
        [ShowNativeProperty] private int TotalWeight 
        {
            get
            {
                int total = 0;
                foreach (var item in typesWeight)
                {
                    total += item.weight;
                }
                return total;
            }
        }

        public Balance()
        {
            typesWeight = new List<BlockType_Weight>();
        }

        public Balance(List<BlockType_Weight> _typesWeight)
        {
            typesWeight = _typesWeight;
        }

        public ABlockType GetRandomBlockType()
        {
            int weightIndex = new System.Random().Next(0, TotalWeight);

            int currentWeightIndex = 0;
            foreach (var item in typesWeight)
            {
                currentWeightIndex += item.weight;
                if (currentWeightIndex >= weightIndex)
                {
                    return item.blockType;
                }
            }

            return new BasicBlockType();
        }

        public Balance Clone()
        {
            return (Balance)this.MemberwiseClone();
        }
    }
}