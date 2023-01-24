using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.UnitTests
{
    public class GravitySystemTests
    {
        [Test]
        public void Execute_OneBlockOneEmptyCellUnder_BlockMovesDown()
        {
            Level level = new(1,2);
            Block block = level.gameBoard.cells[0, 0].SpawnBlock(new BlueBlockType());
            level.gameBoard.cells[0, 1].SetBlock(null);
            GravitySystem gravitySystem = new GravitySystem(level);

            gravitySystem.Execute();

            Assert.AreEqual(null, level.gameBoard.cells[0, 0].block);
            Assert.AreEqual(block, level.gameBoard.cells[0, 1].block);
        }

        [Test]
        public void Execute_OneBlockNoEmptyCellUnder_NoChange()
        {
            Level level = new(1, 2);
            Block blockA = level.gameBoard.cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = level.gameBoard.cells[0, 1].SpawnBlock(new BlueBlockType());
            GravitySystem gravitySystem = new GravitySystem(level);

            gravitySystem.Execute();

            Assert.AreEqual(blockA, level.gameBoard.cells[0, 0].block);
            Assert.AreEqual(blockB, level.gameBoard.cells[0, 1].block); 
        }

        [Test]
        public void Execute_TwoBlocksEmptyCellUnder_TwoBlocksMoveDown()
        {
            Level level = new(1, 3);
            Block blockA = level.gameBoard.cells[0, 0].SpawnBlock(new BlueBlockType());
            Block blockB = level.gameBoard.cells[0, 1].SpawnBlock(new BlueBlockType());
            level.gameBoard.cells[0, 2].SetBlock(null);
            GravitySystem gravitySystem = new GravitySystem(level);

            gravitySystem.Execute();

            Assert.AreEqual(null, level.gameBoard.cells[0, 0].block);
            Assert.AreEqual(blockA, level.gameBoard.cells[0, 1].block); 
            Assert.AreEqual(blockB, level.gameBoard.cells[0, 2].block); 
        }

        [Test]
        public void Execute_AllEmptyCells_NoChange()
        {
            Level level = new(1, 2);
            level.gameBoard.cells[0, 0].SetBlock(null);
            level.gameBoard.cells[0, 1].SetBlock(null);
            GravitySystem gravitySystem = new GravitySystem(level);

            gravitySystem.Execute();

            Assert.AreEqual(null, level.gameBoard.cells[0, 0].block);
            Assert.AreEqual(null, level.gameBoard.cells[0, 1].block);
        }
    }
}