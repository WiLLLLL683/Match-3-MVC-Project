using Model.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class BlockType_Weight
    {
        [SerializeReference, SubclassSelector]
        public ABlockType blockType;
        public int weight;
    }

    [Serializable]
    public class BlockType_Weight_Dictionary
    {
        public List<BlockType_Weight> data;
    }


    [CreateAssetMenu(fileName = "Balance", menuName = "Data/Balance")]
    public class Balance: ScriptableObject
    {
        [SerializeField] private BlockType_Weight_Dictionary typesWeight;
        private int totalWeight;

        public Balance()
        {
            typesWeight = new BlockType_Weight_Dictionary();

            totalWeight = 0;
            foreach (var item in typesWeight.data)
            {
                //totalWeight += item.Value;
            }
        }

        public Balance(Dictionary<ABlockType, int> _typesWeight)
        {
            //typesWeight = _typesWeight;

            totalWeight = 0;
            foreach (var item in typesWeight.data)
            {
                //totalWeight += item.Value;
            }
        }

        public ABlockType GetRandomBlockType()
        {
            int weightIndex = new System.Random().Next(0, totalWeight);

            int currentWeightIndex = 0;
            foreach (var item in typesWeight.data)
            {
                //currentWeightIndex += item.Value;
                if (currentWeightIndex >= weightIndex)
                {
                    //return item.Key;
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