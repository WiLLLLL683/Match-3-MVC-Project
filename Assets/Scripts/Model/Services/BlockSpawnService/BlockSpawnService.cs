using Model.Factories;
using Model.Objects;
using System;

namespace Model.Services
{
    public class BlockSpawnService : IBlockSpawnService
    {
        public event Action<Block> OnBlockSpawn;

        private readonly Game game;
        private readonly IBlockFactory blockFactory;
        private readonly IBlockTypeFactory blockTypeFactory;
        private readonly IValidationService validationService;
        private readonly IBlockChangeTypeService changeTypeService;
        private readonly ICellSetBlockService setBlockService;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        public BlockSpawnService(Game game, IBlockFactory blockFactory, IBlockTypeFactory blockTypeFactory, IValidationService validationService, IBlockChangeTypeService changeTypeService, ICellSetBlockService setBlockService)
        {
            this.game = game;
            this.blockFactory = blockFactory;
            this.validationService = validationService;
            this.blockTypeFactory = blockTypeFactory;
            this.changeTypeService = changeTypeService;
            this.setBlockService = setBlockService;
        }

        public void FillHiddenRows()
        {
            for (int y = GameBoard.HiddenRowsStartIndex; y < GameBoard.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < GameBoard.Cells.GetLength(0); x++)
                {
                    if (!validationService.CellIsEmptyAt(new(x, y)))
                        continue;

                    SpawnRandomBlock(GameBoard.Cells[x, y]);
                }
            }
        }

        public void FillGameBoard()
        {
            for (int y = 0; y < GameBoard.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < GameBoard.Cells.GetLength(0); x++)
                {
                    if (!validationService.CellIsEmptyAt(new(x, y)))
                        continue;

                    SpawnRandomBlock(GameBoard.Cells[x, y]);
                }
            }
        }

        public void SpawnRandomBlock_WithOverride(Cell cell)
        {
            IBlockType type = blockTypeFactory.CreateRandom();
            SpawnBlock_WithOverride(cell, type);
        }

        public void SpawnBlock_WithOverride(Cell cell, IBlockType type)
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
            IBlockType type = blockTypeFactory.CreateRandom();
            SpawnBlock(cell, type);
        }

        private void SpawnBlock(Cell cell, IBlockType type)
        {
            var block = blockFactory.Create(type, cell.Position);
            setBlockService.SetBlock(cell, block);
            GameBoard.Blocks.Add(block);
            OnBlockSpawn?.Invoke(block);
        }
    }
}