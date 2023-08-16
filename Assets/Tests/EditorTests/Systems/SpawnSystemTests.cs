using System.Collections;
using System.Collections.Generic;
using Data;
using Model.Objects;
using Model.Systems;
using NUnit.Framework;
using UnitTests;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Systems.UnitTests
{
    public class SpawnSystemTests
    {
        [Test]
        public void SpawnTopLine_2cellsGameBoard_OnlyTopLineSpawned()
        {
            Level level = new Level(1, 2);
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnTopLine();

            Assert.IsFalse(level.gameBoard.cells[0, 0].IsEmpty);
            Assert.IsTrue(level.gameBoard.cells[0, 1].IsEmpty);
        }

        [Test]
        public void SpawnTopLine_1cellGameBoard_OnlyTopLineSpawned()
        {
            Level level = new Level(1, 1);
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnTopLine();

            Assert.IsFalse(level.gameBoard.cells[0, 0].IsEmpty);
        }

        [Test]
        public void SpawnTopLine_9cellsGameBoard_OnlyTopLineSpawned()
        {
            Level level = new Level(3, 3);
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnTopLine();

            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++) //первая полоса заполнена
            {
                Assert.IsFalse(level.gameBoard.cells[x, 0].IsEmpty);
            }

            for (int y = 1; y < level.gameBoard.cells.GetLength(1); y++) //остольные полосы пусты
            {
                for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
                {
                    Assert.IsTrue(level.gameBoard.cells[0, 1].IsEmpty);
                }
            }
        }

        [Test]
        public void SpawnBonusBlock_EmptyCell_BonusBlockSpawned()
        {
            Level level = new Level(1, 1);
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnBonusBlock(new BasicBlockType(), level.gameBoard.cells[0,0]);

            Assert.IsFalse(level.gameBoard.cells[0,0].IsEmpty);
            Assert.That(level.gameBoard.cells[0, 0].Block.Type is BasicBlockType);
        }

        [Test]
        public void SpawnBonusBlock_FullCell_BlockTypeChanged()
        {
            Level level = new Level(1, 1);
            level.gameBoard.cells[0, 0].SpawnBlock(new BasicBlockType());
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnBonusBlock(new BasicBlockType(), level.gameBoard.cells[0, 0]);

            Assert.That(level.gameBoard.cells[0, 0].Block.Type is BasicBlockType);
        }

        [Test]
        public void SpawnBonusBlock_NotPlayableCell_Nothing()
        {
            Level level = new Level(1, 1);
            level.gameBoard.cells[0, 0].ChangeType(TestUtils.NotPlayableCellType);
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnBonusBlock(new BasicBlockType(), level.gameBoard.cells[0, 0]);

            Assert.IsTrue(level.gameBoard.cells[0, 0].IsEmpty);
        }
    }
}