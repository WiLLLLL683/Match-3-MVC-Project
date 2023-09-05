using System.Collections.Generic;

namespace Model.Objects
{
    /// <summary>
    /// Данные о вероятностях спавна разных типов блоков
    /// </summary>
    public class Balance
    {
        public List<(IBlockType type, int weight)> typesWeight = new();
        public IBlockType defaultBlockType;
        public int totalWeight;

        public void SetWeights(List<(IBlockType type, int weight)> typesWeight)
        {
            this.typesWeight = typesWeight;
            CalculateTotalWeight();
        }

        /// <summary>
        /// Получить рандомный тип блока с заданными вероятностями
        /// </summary>
        public IBlockType GetRandomBlockType()
        {
            int weightIndex = new System.Random().Next(0, totalWeight);

            int currentWeightIndex = 0;
            foreach (var (type, weight) in typesWeight)
            {
                currentWeightIndex += weight;
                if (currentWeightIndex >= weightIndex)
                {
                    return type;
                }
            }

            return defaultBlockType;
        }

        private void CalculateTotalWeight()
        {
            totalWeight = 0;

            foreach (var (type, weight) in typesWeight)
            {
                totalWeight += weight;
            }
        }
    }
}