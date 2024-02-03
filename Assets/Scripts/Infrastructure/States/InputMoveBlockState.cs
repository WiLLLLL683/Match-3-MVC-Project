using Config;
using Cysharp.Threading.Tasks;
using Infrastructure.Commands;
using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт кор-игры для изменения модели в ответ на инпут(перемещение блока)
    /// PayLoad(Vector2Int, Directions) - позиция блока направление перемещения
    /// </summary>
    public class InputMoveBlockState : IPayLoadedState<(Vector2Int startPos, Directions direction)>
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockActivateService activationService;
        private readonly IBlockMatchService matchService;
        private readonly IBlockMoveService moveService;
        private readonly IBlockDestroyService destroyService;
        private readonly IWinLoseService winLoseService;
        private readonly ICounterTarget turnTarget;

        private Vector2Int startPos;
        private Vector2Int targetPos;
        private Directions direction;

        public InputMoveBlockState(IStateMachine stateMachine,
            IBlockActivateService activationService,
            IBlockMatchService matchService,
            IBlockMoveService moveService,
            IBlockDestroyService destroyService,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.activationService = activationService;
            this.matchService = matchService;
            this.moveService = moveService;
            this.destroyService = destroyService;
            this.winLoseService = winLoseService;
            this.turnTarget = configProvider.Turn.CounterTarget;
        }

        public async UniTask OnEnter((Vector2Int startPos, Directions direction) payLoad, CancellationToken token)
        {
            startPos = payLoad.startPos;
            direction = payLoad.direction;
            targetPos = startPos + direction.ToVector2Int();

            if (direction == Directions.Zero)
            {
                stateMachine.EnterState<WaitState>();
                return;
            }

            if (!TryMoveBlock())
            {
                stateMachine.EnterState<WaitState>();
                return;
            }

            if (!activationService.TryActivateBlock(targetPos) && !TryFindMatches())
            {
                UnMoveBlock();
                stateMachine.EnterState<WaitState>();
                return;
            }

            winLoseService.TryDecreaseCount(turnTarget);
            stateMachine.EnterState<DestroyState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }

        private bool TryMoveBlock() => moveService.Move(startPos, targetPos);
        private bool UnMoveBlock() => moveService.Move(targetPos, startPos);

        private bool TryFindMatches()
        {
            HashSet<Cell> matches = matchService.FindAllMatches();

            foreach (Cell cell in matches)
            {
                destroyService.MarkToDestroy(cell.Block.Position);
            }

            return matches.Count > 0;
        }
    }
}