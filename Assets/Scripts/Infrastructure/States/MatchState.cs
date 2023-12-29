using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Model.Objects;
using Model.Services;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт кор-игры для проверки совпадений.
    /// При наличии совпадений - переход в DestroyState.
    /// При отсутствии - переход в WaitState
    /// </summary>
    public class MatchState : IState
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockMatchService matchService;

        public MatchState(IStateMachine stateMachine, IBlockMatchService matchService)
        {
            this.stateMachine = stateMachine;
            this.matchService = matchService;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            HashSet<Cell> matches = matchService.FindAllMatches();

            if (matches.Count > 0)
            {
                stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
                return;
            }

            stateMachine.EnterState<WaitState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}