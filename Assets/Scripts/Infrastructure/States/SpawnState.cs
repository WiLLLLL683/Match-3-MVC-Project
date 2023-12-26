using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Model.Objects;
using Model.Services;
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
            int emptyCellsCount = validationService.FindEmptyCells().Count;

            while (emptyCellsCount > 0)
            {
                gravityService.Execute();
                spawnService.FillHiddenRows();
                emptyCellsCount = validationService.FindEmptyCells().Count;
            }

            stateMachine.EnterState<MatchState>();
        }

        public async UniTask OnExit()
        {

        }
    }
}