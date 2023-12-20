using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using Model.Services;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт кор-игры для создания моделей блоков в рядах скрытых клеток.
    /// Далее переход в GravityState.
    /// </summary>
    public class SpawnState : StateBase
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

        public override IEnumerator OnEnter()
        {
            int emptyCellsCount = validationService.FindEmptyCells().Count;

            while (emptyCellsCount > 0)
            {
                gravityService.Execute();
                spawnService.FillInvisibleRows();
                emptyCellsCount = validationService.FindEmptyCells().Count;
            }

            stateMachine.EnterState<MatchState>();
            yield break;
        }
    }
}