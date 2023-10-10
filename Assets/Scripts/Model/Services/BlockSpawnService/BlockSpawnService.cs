using Model.Factories;
using Model.Objects;

namespace Model.Services
{
    public class BlockSpawnService : IBlockSpawnService
    {
        private readonly IBlockFactory blockFactory;
        private readonly IValidationService validationService;
        private readonly IRandomBlockTypeService randomService;
        private GameBoard gameBoard;

        public BlockSpawnService(IBlockFactory blockFactory, IValidationService validationService, IRandomBlockTypeService randomService)
        {
            this.blockFactory = blockFactory;
            this.validationService = validationService;
            this.randomService = randomService;
        }

        public void SetLevel(GameBoard gameBoard) => this.gameBoard = gameBoard;

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
            BlockType type = randomService.GetRandomBlockType();
            SpawnBlock_WithOverride(type, cell);
        }

        private void SpawnRandomBlock(Cell cell)
        {
            BlockType type = randomService.GetRandomBlockType();
            SpawnBlock(type, cell);
        }

        public void SpawnBlock_WithOverride(BlockType type, Cell cell)
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

                cell.Block.SetType(type);
            }
        }

        private void SpawnBlock(BlockType type, Cell cell)
        {
            Block block = blockFactory.Create(type, cell.Position);
            cell.SetBlock(block);
            gameBoard.RegisterBlock(block);
        }
    }
}