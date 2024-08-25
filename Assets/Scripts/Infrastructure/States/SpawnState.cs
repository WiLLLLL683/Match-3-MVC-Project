using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Model.Objects;
using Model.Services;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт кор-игры для создания моделей блоков в рядах скрытых клеток.
    /// Далее переход в GravityState.
    /// </summary>
    public class SpawnState : IState
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockSpawnService spawnService;
        private readonly IBlockGravityService gravityService;
        private readonly IValidationService validationService;

        private const int MAX_ITERATIONS_COUNT = 100;

        private CancellationTokenSource internalCts;

        public SpawnState(IStateMachine stateMachine,
            IBlockSpawnService spawnService,
            IBlockGravityService gravityService,
            IValidationService validationService)
        {
            this.stateMachine = stateMachine;
            this.spawnService = spawnService;
            this.gravityService = gravityService;
            this.validationService = validationService;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            internalCts?.Dispose();
            internalCts = new();
            CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(internalCts.Token, token);

            for (int i = 0; i < MAX_ITERATIONS_COUNT; i++)
            {
                var emptyCells = validationService.FindEmptyCellsInPlayArea();
                if (emptyCells.Count == 0)
                    break;

                await gravityService.Execute(emptyCells, linkedCts.Token);
                spawnService.FillHiddenRows();
            }

            stateMachine.EnterState<MatchState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {
            internalCts.Cancel();
        }
    }
}