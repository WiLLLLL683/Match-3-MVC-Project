using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// Система для спавна новых блоков - вверху уровня при нехватке блоков ниже, бонусных блоков по команде
    /// </summary>
    public class SpawnSystem
    {
        private Level level;

        public SpawnSystem(Level _level)
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
                ChangeBlockTypeAction changeTypeAction = new ChangeBlockTypeAction(_type, _cell.block);
                changeTypeAction.Execute();
            }
        }
    }
}