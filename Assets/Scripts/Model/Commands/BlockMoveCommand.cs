using Model.Objects;
using Model.Services;
using UnityEngine;

namespace Model.Commands
{
    /// <summary>
    /// Смена блоков местами в двух клетках
    /// </summary>
    public class BlockMoveCommand : ICommand
    {
        private readonly Vector2Int startPosition;
        private readonly Vector2Int targetPosition;
        private readonly IBlockMoveService blockMoveService;

        public BlockMoveCommand(Vector2Int startPosition, Vector2Int targetPosition, IBlockMoveService blockMoveService)
        {
            this.startPosition = startPosition;
            this.targetPosition = targetPosition;
            this.blockMoveService = blockMoveService;
        }

        public void Execute() => blockMoveService.Move(startPosition, targetPosition);
        public void Undo() => blockMoveService.Move(targetPosition, startPosition);
    }
}
