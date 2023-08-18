using Data;
using Model.Objects;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class BlockSpawnService : IBlockSpawnService
    {
        private readonly IBlockFactory blockFactory;
        private GameBoard gameBoard;
        private Balance balance;

        public BlockSpawnService(IBlockFactory blockFactory)
        {
            this.blockFactory = blockFactory;
        }

        public void SetLevel(GameBoard gameBoard, Balance balance)
        {
            this.gameBoard = gameBoard;
            this.balance = balance;
        }

        public void FillInvisibleRows()
        {
            for (int y = 0; y < gameBoard.RowsOfInvisibleCells; y++)
            {
                for (int x = 0; x < gameBoard.cells.GetLength(0); x++)
                {
                    if (!CellIsReadyToSpawn(gameBoard.cells[x, y]))
                        continue;

                    SpawnRandomBlock(gameBoard.cells[x, y]);
                }
            }
        }

        public void FillGameBoard()
        {
            for (int y = 0; y < gameBoard.cells.GetLength(1); y++)
            {
                for (int x = 0; x < gameBoard.cells.GetLength(0); x++)
                {
                    if (!CellIsReadyToSpawn(gameBoard.cells[x, y]))
                        continue;
                    
                    SpawnRandomBlock(gameBoard.cells[x, y]);
                }
            }
        }

        public void SpawnBlock_WithOverride(IBlockType type, Cell cell)
        {
            //TODO валидация клетки

            if (CellIsReadyToSpawn(cell))
            {
                SpawnBlock(type, cell);
            }
            else
            {
                cell.Block.ChangeType(type);
            }
        }

        public void SpawnRandomBlock_WithOverride(Cell cell)
        {
            IBlockType type = balance.GetRandomBlockType();
            SpawnBlock_WithOverride(type, cell);
        }




        private void SpawnRandomBlock(Cell cell)
        {
            IBlockType type = balance.GetRandomBlockType();
            SpawnBlock(type, cell);
        }

        private void SpawnBlock(IBlockType type, Cell cell)
        {
            Block block = blockFactory.Create(type, cell);
            cell.SetBlock(block);
            gameBoard.RegisterBlock(block);
        }

        private bool CellIsReadyToSpawn(Cell cell) => cell.CanContainBlock && cell.IsEmpty;
    }
}