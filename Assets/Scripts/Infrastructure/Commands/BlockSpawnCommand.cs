using Model.Objects;
using Model.Services;

namespace Infrastructure.Commands
{
    /// <summary>
    /// Спавн блока заданного типа в заданной позиции
    /// </summary>
    public class BlockSpawnCommand : CommandBase
    {
        private readonly Cell cell;
        private readonly IBlockType type;
        private readonly IBlockSpawnService spawnService;
        private readonly IBlockDestroyService destroyService;

        public BlockSpawnCommand(Cell cell, IBlockType type, IBlockSpawnService spawnService, IBlockDestroyService destroyService)
        {
            if (cell == null ||
                type == null ||
                spawnService == null ||
                destroyService == null)
            {
                return;
            }

            this.cell = cell;
            this.type = type;
            this.spawnService = spawnService;
            this.destroyService = destroyService;
            isValid = true;
        }

        protected override void OnExecute()
        {
            spawnService.SpawnBlock_WithOverride(cell, type);
            isExecuted = true;
        }

        protected override void OnUndo()
        {
            destroyService.MarkToDestroy(cell.Position);
            destroyService.DestroyAllMarkedBlocks();
        }
    }
}