﻿using Model.Objects;
using UnityEngine;

namespace Model.Factories
{
    public class BlockFactory : IBlockFactory
    {
        public Block Create(BlockType type, Vector2Int position)
        {
            BlockType typeClone = type.Clone();
            return new Block(typeClone, position);
        }
    }
}