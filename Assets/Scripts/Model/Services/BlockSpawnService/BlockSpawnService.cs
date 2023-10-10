using Model.Factories;
using Model.Objects;
using System.Collections.Generic;

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

        public List<IAction> FillInvisibleRows()
        {
            List<IAction> actions = new();

            for (int y = 0; y < gameBoard.RowsOfInvisibleCells; y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    if (!validationService.CellIsEmptyAt(new(x, y)))
                        continue;

                    actions.Add(SpawnRandomBlock(gameBoard.Cells[x, y]));
                }
            }

            return actions;
        }

        public List<IAction> FillGameBoard()
        {
            List<IAction> actions = new();

            for (int y = 0; y < gameBoard.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < gameBoard.Cells.GetLength(0); x++)
                {
                    if (!validationService.CellIsEmptyAt(new(x, y)))
                        continue;

                    actions.Add(SpawnRandomBlock(gameBoard.Cells[x, y]));
                }
            }

            return actions;
        }

        public IAction SpawnRandomBlock_WithOverride(Cell cell)
        {
            BlockType type = randomService.GetRandomBlockType();
            return SpawnBlock_WithOverride(type, cell);
        }

        public IAction SpawnBlock_WithOverride(BlockType type, Cell cell)
        {
            if (!validationService.CellExistsAt(cell.Position))
                return null;

            if (validationService.CellIsEmptyAt(cell.Position))
            {
                return SpawnBlock(type, cell);
            }

            if (validationService.BlockExistsAt(cell.Position))
            {
                return ChangeBlockType(type, cell);
            }

            return null;
        }

        private IAction SpawnRandomBlock(Cell cell)
        {
            BlockType type = randomService.GetRandomBlockType();
            return SpawnBlock(type, cell);
        }

        private IAction SpawnBlock(BlockType type, Cell cell)
        {
            IAction spawnAction = new SpawnBlockAction(gameBoard, type, cell, blockFactory);
            spawnAction.Execute();
            return spawnAction;
        }

        private static IAction ChangeBlockType(BlockType type, Cell cell)
        {
            IAction changeTypeAction = new ChangeBlockTypeAction(type, cell.Block);
            changeTypeAction.Execute();
            return changeTypeAction;
        }
    }
}