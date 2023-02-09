using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// Система для спавна новых блоков
    /// </summary>
    public class SpawnSystem : ISpawnSystem
    {
        private Level level;

        /// <summary>
        /// Обновить данные об уровне
        /// </summary>
        public void SetLevel(Level _level)
        {
            level = _level;
        }

        /// <summary>
        /// спавн новых блоков вверху уровня при нехватке блоков ниже
        /// </summary>
        public void SpawnTopLine()
        {
            for (int x = 0; x < level.gameBoard.Cells.GetLength(0); x++)
            {
                ABlockType type = level.balance.GetRandomBlockType();
                SpawnBlockAction spawnAction = new SpawnBlockAction(level.gameBoard, type, level.gameBoard.Cells[x, 0]);
                spawnAction.Execute();
            }
        }

        /// <summary>
        /// спавн бонусных блоков
        /// </summary>
        public void SpawnBonusBlock(ABlockType _type, Cell _cell)
        {
            if (_cell.IsEmpty)
            {
                SpawnBlockAction spawnAction = new SpawnBlockAction(level.gameBoard, _type, _cell);
                spawnAction.Execute();
            }
            else
            {
                ChangeBlockTypeAction changeTypeAction = new ChangeBlockTypeAction(_type, _cell.Block);
                changeTypeAction.Execute();
            }
        }

        /// <summary>
        /// for tests only
        /// </summary>
        public Level GetLevel() => level;
    }
}