using Model.Objects;

namespace Model.Systems
{
    /// <summary>
    /// Спавн блока заданного типа в заданной позиции
    /// </summary>
    public class SpawnBlockAction : IAction
    {
        private GameBoard gameBoard;
        private IBlockType type;
        private Cell cell;

        public SpawnBlockAction(GameBoard _gameBoard, IBlockType _type, Cell _cell)
        {
            gameBoard = _gameBoard;
            type = _type;
            cell = _cell;
        }

        public void Execute()
        {
            Block block = cell.SpawnBlock(type);
            gameBoard.RegisterBlock(block);
        }

        public void Undo()
        {
            cell.DestroyBlock();
        }
    }
}