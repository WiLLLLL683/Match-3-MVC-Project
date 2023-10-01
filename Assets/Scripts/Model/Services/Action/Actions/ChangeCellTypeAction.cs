using UnityEngine;
using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// смена типа клетки
    /// </summary>
    public class ChangeCellTypeAction : IAction
    {
        private readonly Cell cell;
        private readonly ICellType targetType;
        private readonly ICellType previousType;

        public ChangeCellTypeAction(Cell cell, ICellType targetType)
        {
            this.cell = cell;
            this.targetType = targetType;
            previousType = this.cell.Type;
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