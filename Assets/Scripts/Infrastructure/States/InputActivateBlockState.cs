using Config;
using Cysharp.Threading.Tasks;
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
    /// Стейт кор-игры для изменения модели в ответ на инпут(нажатие на блок)
    /// PayLoad(Vector2Int) - позиция нажатого блока
    /// </summary>
    public class InputActivateBlockState : IPayLoadedState<Vector2Int>
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockActivateService activateService;
        private readonly IBlockMatchService matchService;
        private readonly IWinLoseService winLoseService;
        private readonly IBlockDestroyService destroyService;
        private readonly IValidationService validationService;
        private readonly ICounterTarget turnTarget;

        public InputActivateBlockState(IStateMachine stateMachine,
            IBlockActivateService activateService,
            IBlockMatchService matchService,
            IWinLoseService winLoseService,
            IBlockDestroyService destroyService,
            IValidationService validationService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.activateService = activateService;
            this.matchService = matchService;
            this.winLoseService = winLoseService;
            this.destroyService = destroyService;
            this.validationService = validationService;
            this.turnTarget = configProvider.Turn.CounterTarget;
        }

        public async UniTask OnEnter(Vector2Int position, CancellationToken token)
        {
            Block block = validationService.TryGetBlock(position);
            if (block == null)
            {
                stateMachine.EnterState<WaitState>();
                return;
            }

            await activateService.ActivateBlock(position, Directions.Zero);
            FindMatches();
            CountDownTurn();

            stateMachine.EnterState<DestroyState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }

        private void FindMatches()
        {
            foreach (Cell cell in matchService.FindAllMatches())
            {
                destroyService.MarkToDestroy(cell.Block.Position);
            }
        }

        private void CountDownTurn() => winLoseService.TryDecreaseCount(turnTarget);
    }
}