using Model.Factories;
using Model.Objects;

namespace Model.Services
{
    public class DestroyBlockAction : IAction
    {
        public Block Block;

        private readonly GameBoard gameBoard;
        private readonly Cell cell;
        private readonly IBlockFactory factory;

        public DestroyBlockAction(GameBoard gameBoard, Cell cell, IBlockFactory factory)
        {
            this.gameBoard = gameBoard;
            this.cell = cell;
            this.Block = cell.Block;
            this.factory = factory;
        }

        public void Execute()
        {
            gameBoard.Blocks.Remove(Block);
            cell?.SetEmpty();
        }

        public void Undo()
        {
            var spawnAction = new SpawnBlockAction(gameBoard, Block?.Type, cell, factory);
            spawnAction.Execute();
            Block = spawnAction.Block;
        }
    }
}