using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.Tests
{
    public class GravitySystemTests
    {
        [Test]
        public void Execute_OneBlockOneEmptyCellUnder_BlockMovesDown()
        {
            GameBoard gameboard = new GameBoard(1,2);
            Block block = new Block(new BlueBlockType(),new Vector2Int(0,0));
            gameboard.cells[0, 0].SetBlock(block);
            gameboard.cells[0, 1].SetBlock(null);
            GravitySystem gravitySystem = new GravitySystem(gameboard);

            gravitySystem.Execute();

            Assert.AreEqual(null, gameboard.cells[0, 0].block);
            Assert.AreEqual(block, gameboard.cells[0, 1].block);
        }

        [Test]
        public void Execute_OneBlockNoEmptyCellUnder_NoChange()
        {
            GameBoard gameboard = new GameBoard(1,2);
            Block blockA = new Block(new BlueBlockType(),new Vector2Int(0,0));
            Block blockB = new Block(new BlueBlockType(),new Vector2Int(0,1));
            gameboard.cells[0, 0].SetBlock(blockA);
            gameboard.cells[0, 1].SetBlock(blockB);
            GravitySystem gravitySystem = new GravitySystem(gameboard);

            gravitySystem.Execute();

            Assert.AreEqual(blockA, gameboard.cells[0, 0].block);
            Assert.AreEqual(blockB, gameboard.cells[0, 1].block); 
        }

        [Test]
        public void Execute_TwoBlocksEmptyCellUnder_TwoBlocksMoveDown()
        {
            GameBoard gameboard = new GameBoard(1,3);
            Block blockA = new Block(new BlueBlockType(),new Vector2Int(0,0));
            Block blockB = new Block(new BlueBlockType(),new Vector2Int(0,1));
            gameboard.cells[0, 0].SetBlock(blockA);
            gameboard.cells[0, 1].SetBlock(blockB);
            gameboard.cells[0, 2].SetBlock(null);
            GravitySystem gravitySystem = new GravitySystem(gameboard);

            gravitySystem.Execute();

            Assert.AreEqual(null, gameboard.cells[0, 0].block);
            Assert.AreEqual(blockA, gameboard.cells[0, 1].block); 
            Assert.AreEqual(blockB, gameboard.cells[0, 2].block); 
        }

        [Test]
        public void Execute_AllEmptyCells_NoChange()
        {
            GameBoard gameboard = new GameBoard(1,2);
            gameboard.cells[0, 0].SetBlock(null);
            gameboard.cells[0, 1].SetBlock(null);
            GravitySystem gravitySystem = new GravitySystem(gameboard);

            gravitySystem.Execute();

            Assert.AreEqual(null, gameboard.cells[0, 0].block);
            Assert.AreEqual(null, gameboard.cells[0, 1].block);
        }
    }
}