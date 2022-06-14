using Model.Objects;
using System;
using UnityEngine;

namespace Model
{
    public delegate void DestroyBlock(Block block, EventArgs eventArgs);
    public delegate void EmptyCell(Cell cell, EventArgs eventArgs);
}
