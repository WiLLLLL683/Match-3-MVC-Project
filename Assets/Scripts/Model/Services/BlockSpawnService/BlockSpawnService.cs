using Model.Factories;
using Model.Objects;
using System;

namespace Model.Services
{
    public class BlockSpawnService : IBlockSpawnService
    {
        private readonly IBlockFactory blockFactory;
        private readonly IValidationService validationService;
        private readonly IRandomBlockTypeService randomService;
        private readonly IBlockChangeTypeService changeTypeService;
        private readonly ICellSetBlockService setBlockService;
        private GameBoard gameBoard;

        public event Action<Block> OnBlockSpawn;

        public BlockSpawnService(IBlockFactory blockFactory, IValidationService validationService, IRandomBlockTypeService randomService, IBlockChangeTypeService changeTypeService, ICellSetBlockService setBlockService)
        {
            this.blockFactory = blockFactory;
            this.validationService = validationService;
            this.randomService = randomService;
            this.changeTypeService = changeTypeService;
            this.setBlockService = setBlockService;
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
            SpawnBlock_WithOverride(cell, type);
        }

        public void SpawnBlock_WithOverride(Cell cell, BlockType type)
        {
            if (!validationService.CellExistsAt(cell.Position))
                return;

            if (validationService.CellIsEmptyAt(cell.Position))
            {
                SpawnBlock(cell, type);
            }

            if (validationService.BlockExistsAt(cell.Position))
            {
                changeTypeService.ChangeBlockType(cell, type);
            }
        }

        private void SpawnRandomBlock(Cell cell)
        {
            BlockType type = randomService.GetRandomBlockType();
            SpawnBlock(cell, type);
        }

        private void SpawnBlock(Cell cell, BlockType type)
        {
            var block = blockFactory.Create(type, cell.Position);
            setBlockService.SetBlock(cell, block);
            gameBoard.Blocks.Add(block);
            OnBlockSpawn?.Invoke(block);
        }
    }
}