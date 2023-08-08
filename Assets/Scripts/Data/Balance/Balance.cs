using Model.Objects;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Данные о вероятностях спавна разных типов блоков
    /// </summary>
    [CreateAssetMenu(fileName = "Balance", menuName = "Data/Balance")]
    public class Balance: ScriptableObject
    {
        [SerializeField] private List<BlockType_Weight> typesWeight;
        [ShowNonSerializedField] private int totalWeight;
        [SerializeField] private BlockTypeSO defaultBlockType;

        public Balance()
        {
            typesWeight = new List<BlockType_Weight>();
            totalWeight = CalculateTotalWeight();
        }

        /// <summary>
        /// for tests only
        /// </summary>
        public Balance(List<BlockType_Weight> typesWeight, IBlockType defaultBlockType)
        {
            this.typesWeight = typesWeight;
            this.defaultBlockType = new();
            this.defaultBlockType.blockType = defaultBlockType;
            totalWeight = CalculateTotalWeight();
        }

        private void OnValidate()
        {
            for (int i = 0; i < typesWeight.Count; i++)
            {
                typesWeight[i].LinkBlockTypeToSO();
            }
            totalWeight = CalculateTotalWeight();
        }

        /// <summary>
        /// Получить рандомный тип блока с заданными вероятностями
        /// </summary>
        public IBlockType GetRandomBlockType()
        {
            int weightIndex = new System.Random().Next(0, totalWeight);

            int currentWeightIndex = 0;
            foreach (var item in typesWeight)
            {
                currentWeightIndex += item.weight;
                if (currentWeightIndex >= weightIndex)
                {
                    return item.blockType;
                }
            }

            return defaultBlockType.blockType;
        }

        public Balance Clone()
        {
            return (Balance)this.MemberwiseClone();
        }



        private int CalculateTotalWeight()
        {
            int total = 0;

            foreach (var item in typesWeight)
            {
                total += item.weight;
            }

            return total;
        }
    }
}