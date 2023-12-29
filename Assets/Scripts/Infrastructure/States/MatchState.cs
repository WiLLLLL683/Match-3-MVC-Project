using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Config;
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
        private readonly IConfigProvider configProvider;

        public MatchState(IStateMachine stateMachine, IBlockMatchService matchService, IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.configProvider = configProvider;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            HashSet<Cell> matches = matchService.FindAllMatches();

            if (matches.Count > 0)
            {
                await UniTask.WaitForSeconds(configProvider.Delays.beforeMatchCheck, cancellationToken: token);
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