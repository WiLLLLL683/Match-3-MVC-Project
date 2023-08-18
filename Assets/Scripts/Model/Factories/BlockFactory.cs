using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : IBlockFactory
{
    public Block Create(IBlockType type, Cell cell)
    {
        return new Block(type, cell);
    }
}
