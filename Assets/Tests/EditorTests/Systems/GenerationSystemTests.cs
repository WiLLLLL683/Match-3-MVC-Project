using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using Model.Systems;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.Tests
{
    public class GenerationSystemTests
    {
        [Test]
        public void SpawnTopLine_2cellsGameBoard_OnlyTopLineSpawned()
        {
            GameBoard gameboard = new GameBoard(1, 2);
            GenerationSystem generationSystem = new GenerationSystem(gameboard);

            generationSystem.SpawnTopLine();

            Assert.IsFalse(gameboard.cells[0, 0].isEmpty);
            Assert.IsTrue(gameboard.cells[0, 1].isEmpty);
        }

        [Test]
        public void SpawnTopLine_1cellGameBoard_OnlyTopLineSpawned()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            GenerationSystem generationSystem = new GenerationSystem(gameboard);

            generationSystem.SpawnTopLine();

            Assert.IsFalse(gameboard.cells[0, 0].isEmpty);
        }

        [Test]
        public void SpawnTopLine_9cellsGameBoard_OnlyTopLineSpawned()
        {
            GameBoard gameboard = new GameBoard(3, 3);
            GenerationSystem generationSystem = new GenerationSystem(gameboard);

            generationSystem.SpawnTopLine();

            for (int x = 0; x < gameboard.cells.GetLength(0); x++) //первая полоса заполнена
            {
                Assert.IsFalse(gameboard.cells[x, 0].isEmpty);
            }

            for (int y = 1; y < gameboard.cells.GetLength(1); y++) //остольные полосы пусты
            {
                for (int x = 0; x < gameboard.cells.GetLength(0); x++)
                {
                    Assert.IsTrue(gameboard.cells[0, 1].isEmpty);
                }
            }
        }

        [Test]
        public void SpawnBonusBlock_EmptyCell_BonusBlockSpawned()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            GenerationSystem generationSystem = new GenerationSystem(gameboard);

            generationSystem.SpawnBonusBlock(new BlueBlockType(), new Vector2Int(0,0));

            Assert.IsFalse(gameboard.cells[0,0].isEmpty);
            Assert.That(gameboard.cells[0, 0].block.type is BlueBlockType);
        }

        [Test]
        public void SpawnBonusBlock_FullCell_BlockTypeChanged()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            Block block = new Block(new RedBlockType(), new Vector2Int(0, 0));
            gameboard.cells[0, 0].SetBlock(block);
            GenerationSystem generationSystem = new GenerationSystem(gameboard);

            generationSystem.SpawnBonusBlock(new BlueBlockType(), new Vector2Int(0,0));

            Assert.That(gameboard.cells[0, 0].block.type is BlueBlockType);
        }

        [Test]
        public void SpawnBonusBlock_NotPlayableCell_Nothing()
        {
            GameBoard gameboard = new GameBoard(1, 1);
            gameboard.cells[0, 0].SetType(new NotPlayableCellType());
            GenerationSystem generationSystem = new GenerationSystem(gameboard);

            generationSystem.SpawnBonusBlock(new BlueBlockType(), new Vector2Int(0, 0));

            Assert.IsTrue(gameboard.cells[0, 0].isEmpty);
        }
    }
}