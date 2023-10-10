using System;
using System.Collections.Generic;
using Config;
using Model.Objects;

namespace Model.Services
{
    [Serializable]
    public class RandomBlockTypeService : IRandomBlockTypeService
    {
        private List<BlockType_Weight> typesWeight = new();
        private BlockType defaultBlockType;
        private int totalWeight;

        public void SetLevel(List<BlockType_Weight> typesWeight, BlockType defaultBlockType)
        {
            this.defaultBlockType = defaultBlockType;
            this.typesWeight = typesWeight;
            CalculateTotalWeight();
        }

        /// <summary>
        /// Получить рандомный тип блока с заданными вероятностями
        /// </summary>
        public BlockType GetRandomBlockType()
        {
            int weightIndex = new Random().Next(0, totalWeight);

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