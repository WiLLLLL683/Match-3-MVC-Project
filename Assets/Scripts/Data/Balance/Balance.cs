using Model.Objects;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Данные о вероятностях спавна разных типов блоков
    /// </summary>
    [CreateAssetMenu(fileName = "Balance", menuName = "Data/Balance")]
    public class Balance: ScriptableObject, ICloneable
    {
        public List<BlockType_Weight> typesWeight = new();
        public BlockTypeSO defaultBlockType;
        [ShowNonSerializedField] public int totalWeight;

        public Balance()
        {
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

        public object Clone() => this.MemberwiseClone();



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