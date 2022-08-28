using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// смена типа блока
    /// </summary>
    public class ChangeBlockTypeAction : IAction
    {
        private GameBoard gameBoard;
        private Cell cell;
        private ABlockType targetType;
        private ABlockType previousType;

        public ChangeBlockTypeAction(GameBoard _gameBoard, ABlockType _type, Cell _cell)
        {
            if (_cell.block != null)
            {
                gameBoard = _gameBoard;
                cell = _cell;
                targetType = _type;
                previousType = cell.block.type;
            }
        }

        public void Execute()
        {
            if (gameBoard == null || targetType == null || previousType == null || cell == null)
            {
                Debug.LogError("Invalid input data");
                return;
            }

            cell.block.ChangeType(targetType);
        }

        public void Undo()
        {
            cell.block.ChangeType(previousType);
        }
    }
}