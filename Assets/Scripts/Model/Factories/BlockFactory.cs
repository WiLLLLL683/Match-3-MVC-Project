using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Factories
{
    public class BlockFactory : IBlockFactory
    {
        public Block Create(IBlockType type, Cell cell)
        {
            return new Block(type, cell);
        }
    }
}