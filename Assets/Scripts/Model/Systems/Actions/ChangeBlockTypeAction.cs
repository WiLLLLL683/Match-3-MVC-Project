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
        ABlockType type;
        Vector2Int position;

        public ChangeBlockTypeAction(GameBoard _gameBoard, ABlockType _type, Vector2Int _position)
        {
            gameBoard = _gameBoard;
            type = _type;
            position = _position;
        }

        public void Execute()
        {
            if (gameBoard == null || type == null || position == null)
            {
                Debug.LogError("Invalid input data");
                return;
            }

            ChangeBlockType(type, position);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }

        private void ChangeBlockType(ABlockType _type, Vector2Int _position)
        {
            if (!Helpers.CheckValidBlockByPosition(gameBoard, _position))
                return;

            gameBoard.cells[_position.x, _position.y].block.ChangeType(_type);
        }
    }
}