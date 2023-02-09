using Model.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public delegate void BlockDelegate(Block sender, EventArgs eventArgs);
    public delegate void CellDelegate(Cell sender, EventArgs eventArgs);
    public delegate void GoalDelegate(Counter sender, EventArgs eventArgs);
    public delegate void InputMoveDelegate(Vector2Int _startPos, Directions _direction);
    public delegate void InputBoosterDelegate(IBooster booster);

    //public class EventDispatcher
    //{
    //    public event BlockDelegate OnAnyBlockDestroyed;
    //    public event BlockDelegate OnAnyBlockChangedType;
    //    public event BlockDelegate OnAnyBlockChangedPosition;
    //    public event CellDelegate OnAnyCellDestroyed;
    //    public event CellDelegate OnAnyCellChangedType;
    //    public event CellDelegate OnAnyCellEmpty;
    //    public event GoalDelegate OnAnyGoalUpdate;
    //    public event GoalDelegate OnAnyGoalComplete;
    //    public event GoalDelegate OnAnyRestrictionUpdate;
    //    public event GoalDelegate OnAnyRestrictionComplete;
    //    public event InputMoveDelegate OnInputMove;
    //    public event InputBoosterDelegate OnInputBooster;

    //    public void SubscribeOnLevel(Level _level)
    //    {
    //        SubscribeOnAllBlocks(_level);
    //        SubscribeOnAllCells(_level);
    //        SubscribeOnAllGoals(_level);
    //        SubscribeOnAllRestrictions(_level);
    //    }
    //    private void SubscribeOnAllBlocks(Level _level)
    //    {
    //        //очистить предыдущие подписки
    //        OnAnyBlockDestroyed = delegate { };
    //        OnAnyBlockChangedType = delegate { };
    //        OnAnyBlockChangedPosition = delegate { };

    //        //подписаться на новые блоки
    //        for (int i = 0; i < _level.gameBoard.blocks.Count; i++)
    //        {
    //            _level.gameBoard.blocks[i].OnDestroy += (Block sender, EventArgs eventArgs) => OnAnyBlockDestroyed?.Invoke(sender, eventArgs);
    //            _level.gameBoard.blocks[i].OnTypeChange += (Block sender, EventArgs eventArgs) => OnAnyBlockChangedType?.Invoke(sender, eventArgs);
    //            _level.gameBoard.blocks[i].OnPositionChange += (Block sender, EventArgs eventArgs) => OnAnyBlockChangedPosition?.Invoke(sender, eventArgs);
    //        }
    //    }
    //    private void SubscribeOnAllCells(Level _level)
    //    {
    //        OnAnyCellDestroyed = delegate { };
    //        OnAnyCellChangedType = delegate { };
    //        OnAnyCellEmpty = delegate { };

    //        for (int x = 0; x < _level.gameBoard.cells.GetLength(0); x++)
    //        {
    //            for (int y = 0; y < _level.gameBoard.cells.GetLength(1); y++)
    //            {
    //                _level.gameBoard.cells[x,y].OnDestroy += (Cell sender, EventArgs eventArgs) => OnAnyCellDestroyed?.Invoke(sender, eventArgs);
    //                _level.gameBoard.cells[x,y].OnTypeChange += (Cell sender, EventArgs eventArgs) => OnAnyCellChangedType?.Invoke(sender, eventArgs);
    //                _level.gameBoard.cells[x,y].OnEmpty += (Cell sender, EventArgs eventArgs) => OnAnyCellEmpty?.Invoke(sender, eventArgs);
    //            }
    //        }
    //    }
    //    private void SubscribeOnAllGoals(Level _level)
    //    {
    //        OnAnyGoalUpdate = delegate { };
    //        OnAnyGoalComplete = delegate { };

    //        for (int i = 0; i < _level.goals.Length; i++)
    //        {
    //            _level.goals[i].OnUpdateEvent += (Counter sender, EventArgs eventArgs) => OnAnyGoalUpdate?.Invoke(sender, eventArgs);
    //            _level.goals[i].OnCompleteEvent += (Counter sender, EventArgs eventArgs) => OnAnyGoalComplete?.Invoke(sender, eventArgs);
    //        }
    //    }
    //    private void SubscribeOnAllRestrictions(Level _level)
    //    {
    //        OnAnyRestrictionUpdate = delegate { };
    //        OnAnyRestrictionComplete = delegate { };

    //        for (int i = 0; i < _level.goals.Length; i++)
    //        {
    //            _level.restrictions[i].OnUpdateEvent += (Counter sender, EventArgs eventArgs) => OnAnyRestrictionUpdate?.Invoke(sender, eventArgs);
    //            _level.restrictions[i].OnCompleteEvent += (Counter sender, EventArgs eventArgs) => OnAnyRestrictionComplete?.Invoke(sender, eventArgs);
    //        }
    //    }
    //}
}
