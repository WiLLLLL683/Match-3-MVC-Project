using Model.Services;
using System.Collections;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт кор-игры для запуска гравитации
    /// </summary>
    public class GravityState : IState
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockGravityService gravityService;

        public GravityState(IStateMachine stateMachine, IBlockGravityService gravityService)
        {
            this.stateMachine = stateMachine;
            this.gravityService = gravityService;
        }

        public IEnumerator OnEnter()
        {
            gravityService.Execute();
            stateMachine.EnterState<MatchState>();
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }
    }
}