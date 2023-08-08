using System.Collections;
using System.Collections.Generic;
using Data;
using Model.Objects;
using Model.Systems;
using NUnit.Framework;
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

            Assert.IsFalse(level.gameBoard.Cells[0, 0].IsEmpty);
            Assert.IsTrue(level.gameBoard.Cells[0, 1].IsEmpty);
        }

        [Test]
        public void SpawnTopLine_1cellGameBoard_OnlyTopLineSpawned()
        {
            Level level = new Level(1, 1);
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnTopLine();

            Assert.IsFalse(level.gameBoard.Cells[0, 0].IsEmpty);
        }

        [Test]
        public void SpawnTopLine_9cellsGameBoard_OnlyTopLineSpawned()
        {
            Level level = new Level(3, 3);
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnTopLine();

            for (int x = 0; x < level.gameBoard.Cells.GetLength(0); x++) //первая полоса заполнена
            {
                Assert.IsFalse(level.gameBoard.Cells[x, 0].IsEmpty);
            }

            for (int y = 1; y < level.gameBoard.Cells.GetLength(1); y++) //остольные полосы пусты
            {
                for (int x = 0; x < level.gameBoard.Cells.GetLength(0); x++)
                {
                    Assert.IsTrue(level.gameBoard.Cells[0, 1].IsEmpty);
                }
            }
        }

        [Test]
        public void SpawnBonusBlock_EmptyCell_BonusBlockSpawned()
        {
            Level level = new Level(1, 1);
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnBonusBlock(new BasicBlockType(), level.gameBoard.Cells[0,0]);

            Assert.IsFalse(level.gameBoard.Cells[0,0].IsEmpty);
            Assert.That(level.gameBoard.Cells[0, 0].Block.Type is BasicBlockType);
        }

        [Test]
        public void SpawnBonusBlock_FullCell_BlockTypeChanged()
        {
            Level level = new Level(1, 1);
            level.gameBoard.Cells[0, 0].SpawnBlock(new BasicBlockType());
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnBonusBlock(new BasicBlockType(), level.gameBoard.Cells[0, 0]);

            Assert.That(level.gameBoard.Cells[0, 0].Block.Type is BasicBlockType);
        }

        [Test]
        public void SpawnBonusBlock_NotPlayableCell_Nothing()
        {
            Level level = new Level(1, 1);
            level.gameBoard.Cells[0, 0].ChangeType(new NotPlayableCellType());
            SpawnSystem spawnSystem = new SpawnSystem();
            spawnSystem.SetLevel(level);

            spawnSystem.SpawnBonusBlock(new BasicBlockType(), level.gameBoard.Cells[0, 0]);

            Assert.IsTrue(level.gameBoard.Cells[0, 0].IsEmpty);
        }
    }
}