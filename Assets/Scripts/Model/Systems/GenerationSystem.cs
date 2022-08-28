using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// ������� ��� ������ ����� ������ - ������ ������ ��� �������� ������ ����, �������� ������ �� �������
    /// </summary>
    public class GenerationSystem
    {
        private Level level;

        public GenerationSystem(Level _level)
        {
            level = _level;
        }

        public void SpawnTopLine()
        {
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                ABlockType type = level.balance.GetRandomBlockType();
                SpawnBlockAction spawnAction = new SpawnBlockAction(level.gameBoard, type, level.gameBoard.cells[x,0]);
                spawnAction.Execute();
            }
        }

        public void SpawnBonusBlock(ABlockType _type, Cell _cell)
        {
            if (_cell.isEmpty)
            {
                SpawnBlockAction spawnAction = new SpawnBlockAction(level.gameBoard, _type, _cell);
                spawnAction.Execute();
            }
            else
            {
                ChangeBlockTypeAction changeTypeAction = new ChangeBlockTypeAction(level.gameBoard, _type, _cell);
                changeTypeAction.Execute();
            }
        }
    }
}