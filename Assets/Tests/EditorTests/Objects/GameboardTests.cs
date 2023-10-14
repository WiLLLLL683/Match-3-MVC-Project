using NUnit.Framework;
using TestUtils;

namespace Model.Objects.UnitTests
{
    public class GameBoardTests
    {
        [Test]
        public void RegisterBlock_NewBlock_BlockRegistered()
        {
            GameBoard gameBoard = TestLevelFactory.CreateGameBoard(2, 2, 0);
            Block block = TestBlockFactory.CreateBlock(TestBlockFactory.DEFAULT_BLOCK);

            gameBoard.RegisterBlock(block);

            Assert.AreEqual(block,gameBoard.Blocks[0]);
        }

        [Test]
        public void RegisterBlock_Null_NoBlocksRegistered()
        {
            GameBoard gameBoard = TestLevelFactory.CreateGameBoard(2, 2, 0);

            gameBoard.RegisterBlock(null);

            Assert.AreEqual(0, gameBoard.Blocks.Count);
        }

        [Test]
        public void UnRegisterBlock_DestroyBlock_BlockUnRegistered()
        {
            GameBoard gameBoard = TestLevelFactory.CreateGameBoard(2, 2, 0);
            Block block = TestBlockFactory.CreateBlock(TestBlockFactory.DEFAULT_BLOCK);

            gameBoard.RegisterBlock(block);
            block.Destroy();

            Assert.AreEqual(0,gameBoard.Blocks.Count);
        }
    }
}