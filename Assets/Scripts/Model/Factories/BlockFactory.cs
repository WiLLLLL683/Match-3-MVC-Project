using Model.Objects;
using UnityEngine;

namespace Model.Factories
{
    public class BlockFactory : IBlockFactory
    {
        public Block Create(IBlockType type, Vector2Int position)
        {
            return new Block(type, position);
        }
    }
}