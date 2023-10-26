using Model.Objects;
using Model.Services;
using UnityEngine;

namespace Model.Infrastructure.Commands
{
    /// <summary>
    /// Смена блоков местами в двух клетках
    /// </summary>
    public class BlockMoveCommand : CommandBase
    {
        private readonly Vector2Int startPosition;
        private readonly Vector2Int targetPosition;
        private readonly IBlockMoveService blockMoveService;

        public BlockMoveCommand(Vector2Int startPosition, Vector2Int targetPosition, IBlockMoveService blockMoveService)
        {
            if (startPosition == targetPosition ||
                blockMoveService == null)
            {
                return;
            }

            this.startPosition = startPosition;
            this.targetPosition = targetPosition;
            this.blockMoveService = blockMoveService;
            isValid = true;
        }

        protected override void OnExecute()
        {
            if(blockMoveService.Move(startPosition, targetPosition))
                isExecuted = true;
        }
        protected override void OnUndo() => blockMoveService.Move(targetPosition, startPosition);
    }
}
