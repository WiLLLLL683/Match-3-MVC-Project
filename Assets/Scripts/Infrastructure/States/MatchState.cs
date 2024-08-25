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
        private readonly IBlockDestroyService destroyService;
        private readonly IConfigProvider configProvider;

        public MatchState(IStateMachine stateMachine,
            IBlockMatchService matchService,
            IBlockDestroyService destroyService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.destroyService = destroyService;
            this.configProvider = configProvider;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            HashSet<Cell> matches = matchService.FindAllMatches();

            if (matches.Count == 0)
            {
                stateMachine.EnterState<WaitState>();
                return;
            }

            foreach (Cell cell in matches)
            {
                destroyService.MarkToDestroy(cell.Block.Position);
            }

            await UniTask.WaitForSeconds(configProvider.Delays.beforeMatchCheck, cancellationToken: token);
            stateMachine.EnterState<DestroyState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}