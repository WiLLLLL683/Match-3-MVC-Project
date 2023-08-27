using System.Collections;
using System.Collections.Generic;
using Data;
using NUnit.Framework;
using UnitTests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Objects.UnitTests
{
    public class GameBoardTests
    {
        [Test]
        public void RegisterBlock_NewBlock_BlockRegistered()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2,2);
            Block block = new Block(new BasicBlockType(), gameBoard.cells[0,0]);

            gameBoard.RegisterBlock(block);

            Assert.AreEqual(block,gameBoard.blocks[0]);
        }

        [Test]
        public void RegisterBlock_Null_NoBlocksRegistered()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2, 2);

            gameBoard.RegisterBlock(null);

            Assert.AreEqual(0, gameBoard.blocks.Count);
        }

        [Test]
        public void UnRegisterBlock_DestroyBlock_BlockUnRegistered()
        {
            GameBoard gameBoard = TestUtils.CreateGameBoard(2, 2);
            Block block = new Block(new BasicBlockType(), gameBoard.cells[0,0]);

            gameBoard.RegisterBlock(block);
            block.Destroy();

            Assert.AreEqual(0,gameBoard.blocks.Count);
        }
    }
}