using Model.Objects;
using System;
using UnityEngine;

namespace Model
{
    public delegate void BlockDelegate(Block block, EventArgs eventArgs);
    public delegate void CellDelegate(Cell cell, EventArgs eventArgs);
    public delegate void GoalDelegate(Counter goal, EventArgs eventArgs);
}
