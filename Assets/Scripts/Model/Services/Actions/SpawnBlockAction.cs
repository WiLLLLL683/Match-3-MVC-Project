using Model.Factories;
using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// ����� ����� ��������� ���� � �������� �������
    /// </summary>
    public class SpawnBlockAction : IAction
    {
        private readonly GameBoard gameBoard;
        private readonly BlockType type;
        private readonly Cell cell;
        private readonly IBlockFactory factory;

        private Block block;

        public SpawnBlockAction(GameBoard gameBoard, BlockType type, Cell cell, IBlockFactory factory)
        {
            this.gameBoard = gameBoard;
            this.type = type;
            this.cell = cell;
            this.factory = factory;
        }

        public void Execute()
        {
            block = factory.Create(type, cell.Position);
            cell.SetBlock(block);
            gameBoard.RegisterBlock(block);
        }

        public void Undo()
        {
            new DestroyBlockAction(gameBoard, cell, factory).Execute();
        }
    }
}