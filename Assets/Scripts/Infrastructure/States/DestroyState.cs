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
    public class DestroyState : IState// IPayLoadedState<HashSet<Cell>>
    {
        private readonly Game model;
        private readonly IStateMachine stateMachine;
        private readonly IBlockDestroyService blockDestroyService;
        private readonly IWinLoseService winLoseService;
        private readonly IConfigProvider configProvider;

        public DestroyState(Game model,
            IStateMachine stateMachine,
            IBlockDestroyService blockDestroyService,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.model = model;
            this.stateMachine = stateMachine;
            this.blockDestroyService = blockDestroyService;
            this.winLoseService = winLoseService;
            this.configProvider = configProvider;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            await UniTask.WaitForSeconds(configProvider.Delays.beforeBlockDestroy, cancellationToken: token);

            List<ICounterTarget> destroyedTargets = blockDestroyService.DestroyAllMarkedBlocks();
            for (int i = 0; i < destroyedTargets.Count; i++)
            {
                winLoseService.TryDecreaseCount(destroyedTargets[i]);
            }

            await UniTask.WaitForSeconds(configProvider.Delays.afterBlockDestroy, cancellationToken: token);
            stateMachine.EnterState<SpawnState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}