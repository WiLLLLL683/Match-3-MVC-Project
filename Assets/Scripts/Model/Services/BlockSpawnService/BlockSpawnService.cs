using Model.Factories;
using Model.Objects;

namespace Model.Services
{
    public class BlockSpawnService : IBlockSpawnService
    {
        private readonly IBlockFactory blockFactory;
        private readonly IValidationService validationService;
        private GameBoard gameBoard;
        private Balance balance;

        public BlockSpawnService(IBlockFactory blockFactory, IValidationService validationService)
        {
            this.blockFactory = blockFactory;
            this.validationService = validationService;
        }

        public void SetLevel(GameBoard gameBoard, Balance balance)
        {
            this.gameBoard = gameBoard;
            this.balance = balance;
        }

        public void FillInvisibleRows()
        {
            for (int y = 0; y < gameBoard.RowsOfInvisibleCells; y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    if (!validationService.CellIsEmptyAt(new(x, y)))
                        continue;

                    SpawnRandomBlock(gameBoard.Cells[x, y]);
                }
            }
        }

        public void FillGameBoard()
        {
            for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    if (!validationService.CellIsEmptyAt(new(x, y)))
                        continue;
                    
                    SpawnRandomBlock(gameBoard.Cells[x, y]);
                }
            }
        }

        public void SpawnRandomBlock_WithOverride(Cell cell)
        {
            IBlockType type = balance.GetRandomBlockType();
            SpawnBlock_WithOverride(type, cell);
        }

        private void SpawnRandomBlock(Cell cell)
        {
            IBlockType type = balance.GetRandomBlockType();
            SpawnBlock(type, cell);
        }

        public void SpawnBlock_WithOverride(IBlockType type, Cell cell)
        {
            if (!validationService.CellExistsAt(cell.Position))
                return;

            if (validationService.CellIsEmptyAt(cell.Position))
            {
                SpawnBlock(type, cell);
            }
            else
            {
                if (!validationService.BlockExistsAt(cell.Position))
                    return;

                cell.Block.ChangeType(type);
            }
        }

        private void SpawnBlock(IBlockType type, Cell cell)
        {
            Block block = blockFactory.Create(type, cell);
            cell.SetBlock(block);
            gameBoard.RegisterBlock(block);
        }
    }
}