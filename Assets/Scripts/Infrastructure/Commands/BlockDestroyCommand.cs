using Model.Objects;
using Model.Services;

namespace Infrastructure.Commands
{
    public class BlockDestroyCommand : CommandBase
    {
        private readonly Cell cell;
        private readonly IBlockDestroyService destroyService;
        private readonly IBlockSpawnService blockSpawnService;

        private IBlockType blockType;

        public BlockDestroyCommand(Cell cell, IBlockDestroyService destroyService, IBlockSpawnService blockSpawnService)
        {
            if (cell == null ||
                destroyService == null ||
                blockSpawnService == null)
            {
                return;
            }

            this.cell = cell;
            this.destroyService = destroyService;
            this.blockSpawnService = blockSpawnService;
            isValid = true;
        }

        protected override void OnExecute()
        {
            if (cell.Block == null ||
                cell.Block.Type == null)
            {
                return;
            }

            blockType = cell.Block.Type;
            //destroyService.TryDestroy(cell.Position);
            isExecuted = true;
        }

        protected override void OnUndo()
        {
            blockSpawnService.SpawnBlock_WithOverride(cell, blockType);
        }
    }
}