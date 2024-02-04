using Model.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestUtils;
using UnityEngine;

namespace Model.Objects.UnitTests
{
    public class ShuffleBlocksBoosterTests
    {
        private int blockMovedCount;

        private (GameBoard gameBoard, IBlockDestroyService destroyService, IBlockMoveService moveService, IBooster booster)
            Setup(int xLength, int yLength)
        {
            blockMovedCount = 0;

            var game = TestLevelFactory.CreateGame(xLength, yLength);
            var gameBoard = game.CurrentLevel.gameBoard;
            var validationService = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var destroyService = new BlockDestroyService(game, validationService, setBlockService);
            var moveService = new BlockMoveService(game, validationService, setBlockService);
            moveService.OnPositionChange += (Block _) => blockMovedCount++;
            var booster = new ShuffleBlocksBooster();

            return (gameBoard, destroyService, moveService, booster);
        }

        [Test]
        public void Execute_2Blocks_BlocksShuffled()
        {
            var (gameBoard, destroyService, moveService, booster) = Setup(2, 1);
            var blockA = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[0, 0], gameBoard);
            var blockB = TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[1, 0], gameBoard);

            booster.Execute(new(), destroyService, moveService);

            Assert.AreEqual(new Vector2Int(0,0), blockB.Position);
            Assert.AreEqual(new Vector2Int(1,0), blockA.Position);
            Assert.AreEqual(2, blockMovedCount);
        }

        [Test]
        [TestCase(3, 3)]
        [TestCase(5, 5)]
        [TestCase(10, 10)]
        public void Execute_ManyBlocks_AllBlocksShuffled(int xLength, int yLength)
        {
            var (gameBoard, destroyService, moveService, booster) = Setup(xLength, yLength);
            for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
                {
                    int initialId = x + y * xLength;
                    TestBlockFactory.CreateBlockInCell(initialId, gameBoard.Cells[x, y], gameBoard);
                }
            }

            booster.Execute(new(), destroyService, moveService);

            int blocksChangedPositionCount = 0;
            for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
                {
                    int initialId = x + y * xLength;
                    int currentId = gameBoard.Cells[x, y].Block.Type.Id;

                    if (initialId != currentId)
                        blocksChangedPositionCount++;
                }
            }
            Assert.AreEqual(blocksChangedPositionCount, xLength * yLength);
        }

        [Test]
        [TestCase(3, 3)]
        [TestCase(5, 5)]
        [TestCase(10, 10)]
        public void Execute_WithHiddenCells_HiddenBlocksNotShuffled(int xLength, int yLength)
        {
            var (gameBoard, destroyService, moveService, booster) = Setup(xLength, yLength);
            gameBoard.HiddenRowsStartIndex = yLength - 2;
            for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
                {
                    int initialId = x + y * xLength;
                    TestBlockFactory.CreateBlockInCell(initialId, gameBoard.Cells[x, y], gameBoard);
                }
            }

            booster.Execute(new(), destroyService, moveService);

            int blocksChangedPositionCount = 0;
            for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
                {
                    int initialId = x + y * xLength;
                    int currentId = gameBoard.Cells[x, y].Block.Type.Id;

                    if (initialId != currentId)
                        blocksChangedPositionCount++;
                }
            }
            Assert.AreEqual(blocksChangedPositionCount, xLength * gameBoard.HiddenRowsStartIndex);
        }
    }
}