using Data;
using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    public class SpawnSystem : ISpawnSystem
    {
        private Level level;

        public void SetLevel(Level level) => this.level = level;

        public void SpawnTopLine()
        {
            for (int y = 0; y < level.rowsOfInvisibleCells; y++)
            {
                for (int x = 0; x < level.gameBoard.Cells.GetLength(0); x++)
                {
                    IBlockType type = level.balance.GetRandomBlockType();
                    SpawnBlockAction spawnAction = new SpawnBlockAction(level.gameBoard, type, level.gameBoard.Cells[x, y]);
                    spawnAction.Execute();
                }
            }
        }

        public void SpawnBonusBlock(IBlockType type, Cell cell)
        {
            if (cell.IsEmpty)
            {
                SpawnBlockAction spawnAction = new SpawnBlockAction(level.gameBoard, type, cell);
                spawnAction.Execute();
            }
            else
            {
                ChangeBlockTypeAction changeTypeAction = new ChangeBlockTypeAction(type, cell.Block);
                changeTypeAction.Execute();
            }
        }

        public void SpawnGameBoard()
        {
            for (int x = 0; x < level.gameBoard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.Cells.GetLength(1); y++)
                {
                    SpawnRandomBlock(level.gameBoard.Cells[x, y]);
                }
            }
        }

        public void SpawnRandomBlock(Cell cell)
        {
            IBlockType blockType = level.balance.GetRandomBlockType();
            new SpawnBlockAction(level.gameBoard, blockType, cell).Execute();
        }
    }
}