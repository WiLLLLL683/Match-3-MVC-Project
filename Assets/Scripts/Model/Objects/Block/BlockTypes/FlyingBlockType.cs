using Cysharp.Threading.Tasks;
using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Objects
{
    [Serializable]
    public class FlyingBlockType : IBlockType
    {
        [field: SerializeField] public int Id { get; set; }

        private Game model;
        private IValidationService validationService;
        private IBlockDestroyService destroyService;
        private IBlockMoveService moveService;
        private bool isActivated;

        public async UniTask<bool> Activate(Vector2Int position, Directions direction, BlockTypeDependencies dependencies)
        {
            if (isActivated)
                return false;

            model = dependencies.model;
            validationService = dependencies.validationService;
            destroyService = dependencies.destroyService;
            moveService = dependencies.moveService;

            destroyService.MarkToDestroy(position);
            Block target = FindTarget();

            if (target == null)
                return false;

            destroyService.MarkToDestroy(target.Position);
            await moveService.FlyAsync(position, target.Position);
            isActivated = true;
            return true;
        }

        public IBlockType Clone() => (IBlockType)MemberwiseClone();

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