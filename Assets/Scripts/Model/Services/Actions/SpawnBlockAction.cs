using Model.Factories;
using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// Спавн блока заданного типа в заданной позиции
    /// </summary>
    public class SpawnBlockAction : IAction
    {
        public Block Block;

        private readonly GameBoard gameBoard;
        private readonly BlockType type;
        private readonly Cell cell;
        private readonly IBlockFactory factory;

        public SpawnBlockAction(GameBoard gameBoard, BlockType type, Cell cell, IBlockFactory factory)
        {
            this.gameBoard = gameBoard;
            this.type = type;
            this.cell = cell;
            this.factory = factory;
        }

        public void Execute()
        {
            Block = factory.Create(type, cell.Position);
            cell.SetBlock(Block);
            gameBoard.Blocks.Add(Block);
        }

        public void Undo()
        {
            new DestroyBlockAction(gameBoard, cell, factory).Execute();
        }
    }
}