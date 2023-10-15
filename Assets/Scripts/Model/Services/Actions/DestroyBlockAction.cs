using Model.Factories;
using Model.Objects;

namespace Model.Services
{
    public class DestroyBlockAction : IAction
    {
        private readonly GameBoard gameBoard;
        private readonly Cell cell;
        private readonly Block block;
        private readonly IBlockFactory factory;

        public DestroyBlockAction(GameBoard gameBoard, Cell cell, IBlockFactory factory)
        {
            this.gameBoard = gameBoard;
            this.cell = cell;
            this.block = cell.Block;
            this.factory = factory;
        }

        public void Execute()
        {
            block?.Destroy();
            cell?.SetEmpty();
        }

        public void Undo()
        {
            new SpawnBlockAction(gameBoard, block?.Type, cell, factory).Execute();
        }
    }
}