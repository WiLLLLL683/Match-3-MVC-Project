using Config;
using Cysharp.Threading.Tasks;
using Model.Objects;
using System;
using UnityEngine;
using Utils;

namespace Model.Services
{
    public class BlockMoveService : IBlockMoveService
    {
        public event Action<Block> OnPositionChange;
        public event Action<Block, Vector2Int> OnFlyStarted;

        private readonly Game game;
        private readonly IValidationService validation;
        private readonly ICellSetBlockService setBlockService;
        private readonly IConfigProvider configProvider;

        private GameBoard GameBoard => game.CurrentLevel.gameBoard;

        public BlockMoveService(Game game,
            IValidationService validationService,
            ICellSetBlockService setBlockService,
            IConfigProvider configProvider)
        {
            this.game = game;
            this.validation = validationService;
            this.setBlockService = setBlockService;
            this.configProvider = configProvider;
        }

        public bool Move(Vector2Int startPosition, Directions direction)
        {
            Vector2Int targetPosition = startPosition + direction.ToVector2Int();
            return Move(startPosition, targetPosition);
        }

        public bool Move(Vector2Int startPosition, Vector2Int targetPosition)
        {
            if (!validation.BlockExistsAt(startPosition))
                return false;

            if (!validation.CellExistsAt(targetPosition))
                return false;

            Cell startCell = GameBoard.Cells[startPosition.x, startPosition.y];
            Cell targetCell = GameBoard.Cells[targetPosition.x, targetPosition.y];

            SwapTwoBlocks(startCell, targetCell);
            return true;
        }

        public async UniTask FlyAsync(Vector2Int startPosition, Vector2Int targetPosition)
        {
            if (!validation.BlockExistsAt(startPosition))
                return;

            if (!validation.CellExistsAt(targetPosition))
                return;

            Block block = validation.TryGetBlock(startPosition);
            Cell startCell = GameBoard.Cells[startPosition.x, startPosition.y];
            Cell targetCell = GameBoard.Cells[targetPosition.x, targetPosition.y];
            OnFlyStarted?.Invoke(block, targetPosition);
            await UniTask.WaitForSeconds(configProvider.Block.blockFlyDuration);
            setBlockService.SetEmpty(startCell);
            setBlockService.SetBlock(targetCell, block);
        }

        public void ShuffleAllBlocks()
        {
            var blockInPlayArea = validation.FindAllBlocksInPlayArea();

            //Fisher–Yates shuffle
            for (int i = blockInPlayArea.Count - 1; i >= 1; i--)
            {
                int random = UnityEngine.Random.Range(0, i);
                Move(blockInPlayArea[i].Position, blockInPlayArea[random].Position);
            }
        }

        private void SwapTwoBlocks(Cell cellA, Cell cellB)
        {
            if (cellA != null && cellB != null)
            {
                Block tempBlock = cellA.Block;
                setBlockService.SetBlock(cellA, cellB.Block);
                setBlockService.SetBlock(cellB, tempBlock);

                OnPositionChange?.Invoke(cellA.Block);
                OnPositionChange?.Invoke(cellB.Block);
            }
        }
    }
}