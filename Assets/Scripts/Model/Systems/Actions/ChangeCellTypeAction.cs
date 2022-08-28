using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// смена типа клетки
    /// </summary>
    public class ChangeCellTypeAction : IAction
    {
        private Cell cell;
        private ACellType targetType;
        private ACellType previousType;

        public ChangeCellTypeAction(Cell _cell, ACellType _type)
        {
            cell = _cell;
            targetType = _type;
            previousType = cell.type;
        }

        public void Execute()
        {
            if (cell == null || targetType == null || previousType == null)
            {
                Debug.LogError("Invalid input data");
                return;
            }

            cell.SetType(targetType);
        }

        public void Undo()
        {
            cell.SetType(previousType);
        }
    }
}