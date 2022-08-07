using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// —истема дл€ спавна новых блоков - вверху уровн€ при нехватке блоков ниже, бонусных блоков по команде
    /// </summary>
    public class GenerationSystem
    {
        private GameBoard gameBoard;

        public GenerationSystem(GameBoard _gameBoard)
        {
            gameBoard = _gameBoard;
        }

        public void SpawnTopLine()
        {
            for (int x = 0; x < gameBoard.cells.GetLength(0); x++)
            {
                SpawnBlockAction spawnAction = new SpawnBlockAction(gameBoard,new RedBlockType(), new Vector2Int(x,0)); //TODO вставить систему рандомного типа блока
                spawnAction.Execute();
            }
        }

        public bool SpawnBonusBlock(ABlockType _type, Vector2Int _position)
        {
            if (!gameBoard.cells[_position.x, _position.y].IsPlayable)
            {
                return false;
            }

            if (!gameBoard.cells[_position.x, _position.y].isEmpty)
            {
                SpawnBlockAction spawnAction = new SpawnBlockAction(gameBoard, _type, _position);
                spawnAction.Execute();
            }
            else
            {
                ChangeBlockTypeAction changeTypeAction = new ChangeBlockTypeAction(gameBoard, _type, _position);
                changeTypeAction.Execute();
            }

            return true;
        }
    }
}