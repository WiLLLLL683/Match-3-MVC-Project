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
        private readonly IStateMachine stateMachine;
        private readonly IBlockDestroyService blockDestroyService;
        private readonly IBlockActivateService blockActivateService;
        private readonly IWinLoseService winLoseService;
        private readonly IConfigProvider configProvider;

        public DestroyState(IStateMachine stateMachine,
            IBlockDestroyService blockDestroyService,
            IBlockActivateService blockActivateService,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.blockDestroyService = blockDestroyService;
            this.blockActivateService = blockActivateService;
            this.winLoseService = winLoseService;
            this.configProvider = configProvider;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            await UniTask.WaitForSeconds(configProvider.Delays.beforeBlockDestroy, cancellationToken: token);
            await ActivateMarkedBlocks();
            DestroyMarkedBlocks();
            await UniTask.WaitForSeconds(configProvider.Delays.afterBlockDestroy, cancellationToken: token);
            stateMachine.EnterState<SpawnState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }

        private async UniTask ActivateMarkedBlocks()
        {
            List<Block> markedBlocks = blockDestroyService.FindMarkedBlocks();

            for (int i = 0; i < markedBlocks.Count; i++)
            {
                if (markedBlocks[i].isMarkedToDestroy)
                {
                    await blockActivateService.TryActivateBlock(markedBlocks[i].Position, Directions.Zero);
                }
            }
        }

        private void DestroyMarkedBlocks()
        {
            List<ICounterTarget> destroyedTargets = blockDestroyService.DestroyAllMarkedBlocks();

            for (int i = 0; i < destroyedTargets.Count; i++)
            {
                winLoseService.TryDecreaseCount(destroyedTargets[i]);
            }
        }
    }
}