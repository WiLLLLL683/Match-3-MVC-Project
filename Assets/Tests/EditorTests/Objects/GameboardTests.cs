using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Objects.UnitTests
{
    public class GameboardTests
    {
        [Test]
        public void GameboardConstructor_NewBoard_BoardWithEmptyCells()
        {
            GameBoard gameBoard = new GameBoard(2,2);

            int fullCellsCount = 0;
            for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
                {
                    if (!gameBoard.Cells[x, y].IsEmpty)
                        fullCellsCount++;
                }
            }

            Assert.AreEqual(0,fullCellsCount);
        }

        [Test]
        public void RegisterBlock_NewBlock_BlockRegistred()
        {
            GameBoard gameBoard = new GameBoard(2,2);
            Block block = new Block(new BasicBlockType(), gameBoard.Cells[0,0]);

            gameBoard.RegisterBlock(block);

            Assert.AreEqual(block,gameBoard.Blocks[0]);
        }

        [Test]
        public void RegisterBlock_Null_NoBlocksRegistred()
        {
            GameBoard gameBoard = new GameBoard(2,2);

            gameBoard.RegisterBlock(null);

            Assert.AreEqual(0, gameBoard.Blocks.Count);
        }

        [Test]
        public void UnRegisterBlock_DestroyBlock_BlockUnRegistred()
        {
            GameBoard gameBoard = new GameBoard(2,2);
            Block block = new Block(new BasicBlockType(), gameBoard.Cells[0,0]);

            gameBoard.RegisterBlock(block);
            block.Destroy();

            Assert.AreEqual(0,gameBoard.Blocks.Count);
        }

    }
}