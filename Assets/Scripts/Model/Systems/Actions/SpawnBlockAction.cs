using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// Спавн блока заданного типа в заданной позиции
    /// </summary>
    public class SpawnBlockAction : IAction
    {
        private GameBoard gameBoard;
        ABlockType type;
        Vector2Int position; //TODO возможно стоит упростить до ссылки на клетку

        public SpawnBlockAction(GameBoard _gameBoard, ABlockType _type, Vector2Int _position)
        {
            gameBoard = _gameBoard;
            type = _type;
            position = _position;
        }

        public void Execute()
        {
            SpawnBlock(type, position);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }

        private void SpawnBlock(ABlockType _type, Vector2Int _position)
        {
            if (!Helpers.CheckValidCellByPosition(gameBoard, _position))
                return;

            if (gameBoard.cells[_position.x, _position.y].IsPlayable &&
                gameBoard.cells[_position.x, _position.y].isEmpty)
            {
                Block block = new Block(_type, _position);
                gameBoard.cells[_position.x, _position.y].SetBlock(block);
            }
        }
    }
}