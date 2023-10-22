using Model.Objects;
using Model.Services;

namespace Model.Commands
{
    /// <summary>
    /// Спавн блока заданного типа в заданной позиции
    /// </summary>
    public class BlockSpawnCommand : ICommand
    {
        private readonly Cell cell;
        private readonly BlockType type;
        private readonly IBlockSpawnService spawnService;
        private readonly IBlockDestroyService destroyService;

        public BlockSpawnCommand(Cell cell, BlockType type, IBlockSpawnService spawnService, IBlockDestroyService destroyService)
        {
            this.cell = cell;
            this.type = type;
            this.spawnService = spawnService;
            this.destroyService = destroyService;
        }

        public void Execute() => spawnService.SpawnBlock_WithOverride(cell, type);
        public void Undo() => destroyService.DestroyAt(cell.Position);
    }
}