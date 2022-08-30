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
            Level level = new Level(1, 2);
            SpawnSystem generationSystem = new SpawnSystem(level);

            generationSystem.SpawnTopLine();

            Assert.IsFalse(level.gameBoard.cells[0, 0].isEmpty);
            Assert.IsTrue(level.gameBoard.cells[0, 1].isEmpty);
        }

        [Test]
        public void SpawnTopLine_1cellGameBoard_OnlyTopLineSpawned()
        {
            Level level = new Level(1, 1);
            SpawnSystem generationSystem = new SpawnSystem(level);

            generationSystem.SpawnTopLine();

            Assert.IsFalse(level.gameBoard.cells[0, 0].isEmpty);
        }

        [Test]
        public void SpawnTopLine_9cellsGameBoard_OnlyTopLineSpawned()
        {
            Level level = new Level(3, 3);
            SpawnSystem generationSystem = new SpawnSystem(level);

            generationSystem.SpawnTopLine();

            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++) //первая полоса заполнена
            {
                Assert.IsFalse(level.gameBoard.cells[x, 0].isEmpty);
            }

            for (int y = 1; y < level.gameBoard.cells.GetLength(1); y++) //остольные полосы пусты
            {
                for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
                {
                    Assert.IsTrue(level.gameBoard.cells[0, 1].isEmpty);
                }
            }
        }

        [Test]
        public void SpawnBonusBlock_EmptyCell_BonusBlockSpawned()
        {
            Level level = new Level(1, 1);
            SpawnSystem generationSystem = new SpawnSystem(level);

            generationSystem.SpawnBonusBlock(new BlueBlockType(), level.gameBoard.cells[0,0]);

            Assert.IsFalse(level.gameBoard.cells[0,0].isEmpty);
            Assert.That(level.gameBoard.cells[0, 0].block.type is BlueBlockType);
        }

        [Test]
        public void SpawnBonusBlock_FullCell_BlockTypeChanged()
        {
            Level level = new Level(1, 1);
            level.gameBoard.cells[0, 0].SpawnBlock(new RedBlockType());
            SpawnSystem generationSystem = new SpawnSystem(level);

            generationSystem.SpawnBonusBlock(new BlueBlockType(), level.gameBoard.cells[0, 0]);

            Assert.That(level.gameBoard.cells[0, 0].block.type is BlueBlockType);
        }

        [Test]
        public void SpawnBonusBlock_NotPlayableCell_Nothing()
        {
            Level level = new Level(1, 1);
            level.gameBoard.cells[0, 0].ChangeType(new NotPlayableCellType());
            SpawnSystem generationSystem = new SpawnSystem(level);

            generationSystem.SpawnBonusBlock(new BlueBlockType(), level.gameBoard.cells[0, 0]);

            Assert.IsTrue(level.gameBoard.cells[0, 0].isEmpty);
        }
    }
}