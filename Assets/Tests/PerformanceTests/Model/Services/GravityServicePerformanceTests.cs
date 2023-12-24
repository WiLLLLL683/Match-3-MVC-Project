using Model.Objects;
using NUnit.Framework;
using TestUtils;
using Unity.PerformanceTesting;

namespace Model.Services.PerformanceTests
{
    public class GravityServicePerformanceTests
    {
        //private (GameBoard gameBoard, BlockGravityService service) Setup(int xLength, int yLength)
        //{
        //    var game = TestLevelFactory.CreateGame(xLength, yLength);
        //    var validation = new ValidationService(game);
        //    var setBlockService = new CellSetBlockService();
        //    var moveService = new BlockMoveService(game, validation, setBlockService);

        //    var service = new BlockGravityService(game, validation, moveService);

        //    return (game.CurrentLevel.gameBoard, service);
        //}

        [Test, Performance]
        public void InitialService_10x10BlocksMoveDown()
        {
            var game = TestLevelFactory.CreateGame(10, 11);
            var gameBoard = game.CurrentLevel.gameBoard;
            var validation = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var moveService = new BlockMoveService(game, validation, setBlockService);

            var service = new BlockGravityService(game, validation, moveService);

            for (int y = 0; y < gameBoard.Cells.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[x, y], gameBoard);
                }
            }

            Measure.Method(() => service.Execute()).Run();
        }

        [Test, Performance]
        public void GravitateOnlyEmptyCells_10x10BlocksMoveDown()
        {
            var game = TestLevelFactory.CreateGame(10, 11);
            var gameBoard = game.CurrentLevel.gameBoard;
            var validation = new ValidationService(game);
            var setBlockService = new CellSetBlockService();
            var moveService = new BlockMoveService(game, validation, setBlockService);

            var service = new BlockGravityService(game, validation, moveService);

            for (int y = 0; y < gameBoard.Cells.GetLength(1) - 1; y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    TestBlockFactory.CreateBlockInCell(TestBlockFactory.DEFAULT_BLOCK, gameBoard.Cells[x, y], gameBoard);
                }
            }

            Measure.Method(() => service.Execute()).Run();
        }
    }
}