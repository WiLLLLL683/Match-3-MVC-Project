using System.Collections;
using System.Collections.Generic;
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

        public IEnumerator OnEnter()
        {
            HashSet<Cell> matches = matchService.FindAllMatches();

            if (matches.Count > 0)
            {
                stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
            }
            else
            {
                stateMachine.EnterState<WaitState>();
            }

            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }
    }
}