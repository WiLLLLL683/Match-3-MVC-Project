using Model.Objects;
using Model.Services;

namespace Model.Commands
{
    public class BlockDestroyCommand : ICommand
    {
        private readonly Cell cell;
        private readonly IBlockDestroyService destroyService;
        private readonly IBlockSpawnService blockSpawnService;

        private BlockType blockType;

        public BlockDestroyCommand(Cell cell, IBlockDestroyService destroyService, IBlockSpawnService blockSpawnService)
        {
            this.cell = cell;
            this.destroyService = destroyService;
            this.blockSpawnService = blockSpawnService;
        }

        public void Execute()
        {
            blockType = cell.Block.Type;
            destroyService.DestroyAt(cell);
        }

        public void Undo()
        {
            blockSpawnService.SpawnBlock_WithOverride(cell, blockType);
        }
    }
}