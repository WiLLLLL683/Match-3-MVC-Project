using Model.Objects;
using UnityEngine;

namespace Model.Factories
{
    public interface IBlockFactory
    {
        public Block Create(BlockType type, Vector2Int position);
    }
}