using System;
using System.Collections.Generic;
using Config;
using Model.Objects;
using Model.Services;
using Zenject;

namespace Model.Factories
{
    [Serializable]
    public class BlockTypeFactory : IBlockTypeFactory
    {
        private readonly IInstantiator instantiator;

        private List<BlockType_Weight> typesWeight = new();
        private IBlockType defaultBlockType;
        private int totalWeight;

        public BlockTypeFactory(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public void SetLevelConfig(List<BlockType_Weight> typesWeight, IBlockType defaultBlockType)
        {
            this.defaultBlockType = defaultBlockType;
            this.typesWeight = typesWeight;
            CalculateTotalWeight();
        }

        public IBlockType Create(int id)
        {
            IBlockType origin = Find(id);
            return Create(origin);
        }

        public IBlockType Create(IBlockType origin)
        {
            Type type = origin.GetType();
            IBlockType blockType = (IBlockType)instantiator.Instantiate(type);
            blockType.Id = origin.Id;
            return blockType;
        }

        public IBlockType CreateRandom()
        {
            int weightIndex = new Random().Next(0, totalWeight);

            int currentWeightIndex = 0;
            for (int i = 0; i < typesWeight.Count; i++)
            {
                currentWeightIndex += typesWeight[i].weight;

                if (currentWeightIndex >= weightIndex)
                {
                    return Create(typesWeight[i].type);
                }
            }

            return Create(defaultBlockType);
        }

        private void CalculateTotalWeight()
        {
            totalWeight = 0;

            foreach (var item in typesWeight)
            {
                totalWeight += item.weight;
            }
        }

        private IBlockType Find(int id)
        {
            for (int i = 0; i < typesWeight.Count; i++)
            {
                if (typesWeight[i].type.Id == id)
                {
                    return typesWeight[i].type;
                }
            }

            return defaultBlockType;
        }
    }
}