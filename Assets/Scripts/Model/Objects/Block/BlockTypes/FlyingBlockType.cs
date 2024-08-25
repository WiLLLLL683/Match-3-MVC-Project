using Cysharp.Threading.Tasks;
using Model.Services;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Objects
{
    [System.Serializable]
    public class FlyingBlockType : IBlockType
    {
        [field: SerializeField] public int Id { get; set; }
        public bool IsActivatable => true;

        private readonly Game model;
        private readonly IValidationService validationService;
        private readonly IBlockDestroyService destroyService;
        private readonly IBlockMoveService moveService;

        private bool isActivated;

        public FlyingBlockType(Game model, IValidationService validationService, IBlockDestroyService destroyService, IBlockMoveService moveService)
        {
            this.model = model;
            this.validationService = validationService;
            this.destroyService = destroyService;
            this.moveService = moveService;
        }

        public async UniTask Activate(Vector2Int position, Directions direction)
        {
            if (isActivated)
                return;

            //уничтожение креста вокруг начальной позиции
            destroyService.MarkToDestroy(position + Vector2Int.up);
            destroyService.MarkToDestroy(position + Vector2Int.down);
            destroyService.MarkToDestroy(position + Vector2Int.left);
            destroyService.MarkToDestroy(position + Vector2Int.right);
            destroyService.DestroyAllMarkedBlocks();

            //поиск целевой позиции
            Block target = FindTarget();

            if (target == null)
                target = RandomTarget();

            //уничтожение цели и полет к ее позиции
            destroyService.MarkToDestroy(target.Position);
            await moveService.FlyAsync(position, target.Position);

            //уничтожение летающего блока
            destroyService.MarkToDestroy(target.Position);
            isActivated = true;
            return;
        }

        private Block FindTarget()
        {
            ICounterTarget goalType = GetFirstUncompleatedGoal();

            if (goalType == null)
                return null;

            List<Block> goals = validationService.FindAllBlockOfType(goalType.Id);
            if (goals == null || goals.Count == 0)
                return null;

            int random = UnityEngine.Random.Range(0, goals.Count);
            return goals[random];
        }

        private Block RandomTarget()
        {
            Block block = null;

            while (block == null)
            {
                int x = UnityEngine.Random.Range(0, model.CurrentLevel.gameBoard.Cells.GetLength(0));
                int y = UnityEngine.Random.Range(0, model.CurrentLevel.gameBoard.HiddenRowsStartIndex - 1);
                block = validationService.TryGetBlock(new(x, y));
            }

            return block;
        }

        private ICounterTarget GetFirstUncompleatedGoal()
        {
            if (model.CurrentLevel == null || model.CurrentLevel.goals == null)
                return null;

            Counter[] goals = model.CurrentLevel.goals;

            for (int i = 0; i < goals.Length; i++)
            {
                if (!goals[i].IsCompleted)
                {
                    return goals[i].Target;
                }
            }

            return null;
        }
    }
}