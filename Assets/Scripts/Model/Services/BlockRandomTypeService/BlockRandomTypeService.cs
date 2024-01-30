using System;
using System.Collections.Generic;
using Model.Objects;

namespace Model.Services
{
    [Serializable]
    public class BlockRandomTypeService : IBlockRandomTypeService
    {
        private List<BlockType_Weight> typesWeight = new();
        private IBlockType defaultBlockType;
        private int totalWeight;

        public void SetLevelConfig(List<BlockType_Weight> typesWeight, IBlockType defaultBlockType)
        {
            this.defaultBlockType = defaultBlockType;
            this.typesWeight = typesWeight;
            CalculateTotalWeight();
        }

        public IBlockType GetRandomBlockType()
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