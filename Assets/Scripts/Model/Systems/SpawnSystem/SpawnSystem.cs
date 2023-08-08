using Data;
using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// —истема дл€ спавна новых блоков
    /// </summary>
    public class SpawnSystem : ISpawnSystem
    {
        private Level level;

        /// <summary>
        /// ќбновить данные об уровне
        /// </summary>
        public void SetLevel(Level _level)
        {
            level = _level;
        }

        /// <summary>
        /// спавн новых блоков вверху уровн€ при нехватке блоков ниже
        /// </summary>
        public void SpawnTopLine()
        {
            const int y = 0; //верхн€€ лини€
            for (int x = 0; x < level.gameBoard.Cells.GetLength(0); x++)
            {
                IBlockType type = level.balance.GetRandomBlockType();
                SpawnBlockAction spawnAction = new SpawnBlockAction(level.gameBoard, type, level.gameBoard.Cells[x, y]);
                spawnAction.Execute();
            }
        }

        /// <summary>
        /// спавн бонусных блоков
        /// </summary>
        public void SpawnBonusBlock(IBlockType _type, Cell _cell)
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
    }
}