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
    public class SpawnState : IState
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockSpawnService spawnService;

        public SpawnState(IStateMachine stateMachine, IBlockSpawnService spawnService)
        {
            this.stateMachine = stateMachine;
            this.spawnService = spawnService;
        }

        public IEnumerator OnEnter()
        {
            spawnService.FillInvisibleRows();
            stateMachine.EnterState<GravityState>();
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }
    }
}