using System;
using System.Collections.Generic;

namespace Model.Objects
{
    /// <summary>
    /// Данные о вероятностях спавна разных типов блоков
    /// </summary>
    [Serializable]
    public class Balance
    {
        public List<BlockType_Weight> typesWeight = new();
        public BlockType defaultBlockType;
        public int totalWeight;

        public void SetWeights(List<BlockType_Weight> typesWeight)
        {
            this.typesWeight = typesWeight;
            CalculateTotalWeight();
        }

        /// <summary>
        /// Получить рандомный тип блока с заданными вероятностями
        /// </summary>
        public BlockType GetRandomBlockType()
        {
            int weightIndex = new System.Random().Next(0, totalWeight);

            int currentWeightIndex = 0;
            foreach (var item in typesWeight)
            {
                currentWeightIndex += item.weight;
                if (currentWeightIndex >= weightIndex)
                {
                    return item.type;
                }
            }

            return defaultBlockType;
        }

        private void CalculateTotalWeight()
        {
            totalWeight = 0;

            foreach (var item in typesWeight)
            {
                totalWeight += item.weight;
            }
        }
    }
}