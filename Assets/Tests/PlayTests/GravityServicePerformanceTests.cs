using Config;
using Cysharp.Threading.Tasks;
using Model.Objects;
using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TestUtils;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace Model.Services.PerformanceTests
{
    public class GravityServicePerformanceTests
    {
        [UnityTest, Performance]
        public IEnumerator Execute_WithoutPrepare_10x10BlocksMoveDown()
        {
            var game = TestLevelFactory.CreateGame(10, 11);
            var gameBoard = game.CurrentLevel.gameBoard;
            var validation = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var moveService = new BlockMoveService(game, validation, setBlockService);
            var configProvider = Substitute.For<IConfigProvider>();
            configProvider.Delays.betweenBlockGravitation.Returns(0.01f);
            var service = new BlockGravityService(game, validation, moveService, configProvider);

            for (int y = 0; y < gameBoard.Cells.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[x, y], gameBoard);
                }
            }

            yield return service.Execute();
            yield return Measure.Frames().Run();
        }

        [UnityTest, Performance]
        public IEnumerator Execute_PreFoundCells_10x10BlocksMoveDown()
        {
            var game = TestLevelFactory.CreateGame(10, 11);
            var gameBoard = game.CurrentLevel.gameBoard;
            var validation = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var moveService = new BlockMoveService(game, validation, setBlockService);
            var configProvider = Substitute.For<IConfigProvider>();
            configProvider.Delays.betweenBlockGravitation.Returns(0.01f);
            var service = new BlockGravityService(game, validation, moveService, configProvider);

            for (int y = 0; y < gameBoard.Cells.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[x, y], gameBoard);
                }
            }

            List<Cell> cells = validation.FindEmptyCells();

            yield return service.Execute(cells);
            yield return Measure.Frames().Run();
        }
    }
}