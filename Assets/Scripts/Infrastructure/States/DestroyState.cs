using Config;
using Cysharp.Threading.Tasks;
using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт кор-игры для уничтожения блоков в модели.
    /// PayLoad(HashSet<Cell>) - набор клеток для уничтожения блоков в них.
    /// </summary>
    public class DestroyState : IPayLoadedState<HashSet<Cell>>
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockDestroyService blockDestroyService;
        private readonly IWinLoseService winLoseService;
        private readonly IConfigProvider configProvider;

        public DestroyState(IStateMachine stateMachine,
            IBlockDestroyService blockDestroyService,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.blockDestroyService = blockDestroyService;
            this.winLoseService = winLoseService;
            this.configProvider = configProvider;
        }

        public async UniTask OnEnter(HashSet<Cell> payLoad, CancellationToken token)
        {
            await UniTask.WaitForSeconds(configProvider.Delays.beforeBlockDestroy, cancellationToken: token);
            DestroyBlocks(payLoad);
            await UniTask.WaitForSeconds(configProvider.Delays.afterBlockDestroy, cancellationToken: token);
            stateMachine.EnterState<SpawnState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }

        private void DestroyBlocks(HashSet<Cell> matches)
        {
            if (matches == null)
                return;

            foreach (Cell match in matches)
            {
                winLoseService.DecreaseCountIfPossible(match.Block.Type);
                blockDestroyService.DestroyAt(match);
            }
        }
    }
}