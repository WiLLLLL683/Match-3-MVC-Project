using Model.Objects;
using System;

namespace Model.Factories
{
    [Serializable]
    public class BlockType_Weight
    {
        public IBlockType type;
        public int weight;

        public BlockType_Weight(IBlockType type, int weight)
        {
            this.type = type;
            this.weight = weight;
        }
    }
}