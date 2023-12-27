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

        public async UniTask OnEnter()
        {
            for (int i = 0; i < MAX_ITERATIONS_COUNT; i++)
            {
                var emptyCells = validationService.FindEmptyCells();
                Debug.Log($"EmptyCellsCount = {emptyCells.Count}, iteration = {i}");
                if (emptyCells.Count == 0)
                    break;

                await gravityService.Execute(emptyCells);
                spawnService.FillHiddenRows();
            }

            stateMachine.EnterState<MatchState>();
        }

        public async UniTask OnExit()
        {

        }
    }
}