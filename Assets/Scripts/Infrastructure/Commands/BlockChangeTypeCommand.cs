using Model.Objects;
using Model.Services;
using System;

namespace Infrastructure.Commands
{
    /// <summary>
    /// смена типа блока
    /// </summary>
    public class BlockChangeTypeCommand : CommandBase
    {
        private readonly Block block;
        private readonly IBlockType targetType;
        private readonly IBlockType previousType;
        private readonly IBlockChangeTypeService blockChangeTypeService;

        public BlockChangeTypeCommand(Block block, IBlockType targetType, IBlockChangeTypeService blockChangeTypeService)
        {
            if (block == null ||
                targetType == null ||
                blockChangeTypeService == null||
                block.Type == null)
            {
                return;
            }

            this.block = block;
            this.targetType = targetType;
            this.previousType = block.Type;
            this.blockChangeTypeService = blockChangeTypeService;
            isValid = true;
        }

        protected override void OnExecute()
        {
            blockChangeTypeService.ChangeBlockType(block.Position, targetType);
            isExecuted = true;
        }

        protected override void OnUndo()
        {
            blockChangeTypeService.ChangeBlockType(block.Position, previousType);
        }
    }
}