using System.Collections;
using System.Collections.Generic;
using Data;
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
            for (int x = 0; x < gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.cells.GetLength(1); y++)
                {
                    if (!gameBoard.cells[x, y].IsEmpty)
                        fullCellsCount++;
                }
            }

            Assert.AreEqual(0,fullCellsCount);
        }

        [Test]
        public void RegisterBlock_NewBlock_BlockRegistred()
        {
            GameBoard gameBoard = new GameBoard(2,2);
            Block block = new Block(new BasicBlockType(), gameBoard.cells[0,0]);

            gameBoard.RegisterBlock(block);

            Assert.AreEqual(block,gameBoard.blocks[0]);
        }

        [Test]
        public void RegisterBlock_Null_NoBlocksRegistred()
        {
            GameBoard gameBoard = new GameBoard(2,2);

            gameBoard.RegisterBlock(null);

            Assert.AreEqual(0, gameBoard.blocks.Count);
        }

        [Test]
        public void UnRegisterBlock_DestroyBlock_BlockUnRegistred()
        {
            GameBoard gameBoard = new GameBoard(2,2);
            Block block = new Block(new BasicBlockType(), gameBoard.cells[0,0]);

            gameBoard.RegisterBlock(block);
            block.Destroy();

            Assert.AreEqual(0,gameBoard.blocks.Count);
        }

    }
}