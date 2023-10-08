using System;

namespace Model.Objects
{
    [Serializable]
    public class BlockType_Weight
    {
        public BlockType type;
        public int weight;

        public BlockType_Weight(BlockType type, int weight)
        {
            this.type = type;
            this.weight = weight;
        }
    }
}