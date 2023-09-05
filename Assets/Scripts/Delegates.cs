using Model.Objects;
using System;
using UnityEngine;

namespace Model
{
    public delegate void BlockDelegate(Block sender, EventArgs eventArgs);
    public delegate void CellDelegate(Cell sender, EventArgs eventArgs);
    public delegate void GoalDelegate(Counter sender, EventArgs eventArgs);
    public delegate void InputMoveDelegate(Vector2Int _startPos, Directions _direction);
    public delegate void InputBoosterDelegate(IBooster booster);
}
