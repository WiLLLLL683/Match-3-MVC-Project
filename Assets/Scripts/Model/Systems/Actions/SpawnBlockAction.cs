using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// ����� ����� ��������� ���� � �������� �������
    /// </summary>
    public class SpawnBlockAction : IAction
    {
        private GameBoard gameBoard;
        private ABlockType type;
        private Cell cell;

        public SpawnBlockAction(GameBoard _gameBoard, ABlockType _type, Cell _cell)
        {
            gameBoard = _gameBoard;
            type = _type;
            cell = _cell;
        }

        public void Execute()
        {
            Block block = cell.SpawnBlock(type);
            gameBoard.RegisterBlock(block);
        }

        public void Undo()
        {
            cell.DestroyBlock();
        }
    }
}