using Model.Objects;
using UnityEngine;

namespace Model.Factories
{
    public class BlockFactory : IBlockFactory
    {
        public Block Create(IBlockType type, Vector2Int position)
        {
            var typeClone = type.Clone();
            return new Block(typeClone, position);
        }
    }
}