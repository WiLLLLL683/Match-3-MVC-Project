using Model.Objects;
using System;
using UnityEngine;

namespace Model
{
    public delegate void BlockDelegate(Block block, EventArgs eventArgs);
    public delegate void CellDelegate(Cell cell, EventArgs eventArgs);
    public delegate void GoalDelegate(Goal goal, EventArgs eventArgs);
    public delegate void RestrictionDelegate(Restriction goal, EventArgs eventArgs);

}
