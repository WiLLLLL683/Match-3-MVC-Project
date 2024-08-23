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
    /// Стейт кор-игры для уничтожения всех помеченных блоков в модели.
    /// </summary>
    public class DestroyState : IState
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockActivateService blockActivateService;
        private readonly IBlockDestroyService blockDestroyService;
        private readonly IWinLoseService winLoseService;
        private readonly IConfigProvider configProvider;

        private const int MAX_ITERATIONS_COUNT = 100;

        public DestroyState(IStateMachine stateMachine,
            IBlockActivateService blockActivateService,
            IBlockDestroyService blockDestroyService,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.blockActivateService = blockActivateService;
            this.blockDestroyService = blockDestroyService;
            this.winLoseService = winLoseService;
            this.configProvider = configProvider;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            blockDestroyService.OnDestroy += CountTargets;

            for (int i = 0; i < MAX_ITERATIONS_COUNT; i++)
            {
                //проверить на окончание итераций
                if (blockDestroyService.FindMarkedBlocks().Count == 0)
                    break;

                //активировать блоки
                await blockActivateService.ActivateMarkedBlocks();

                //уничтожить блоки
                await UniTask.WaitForSeconds(configProvider.Delays.beforeBlockDestroy, cancellationToken: token);
                blockDestroyService.DestroyAllMarkedBlocks();
                await UniTask.WaitForSeconds(configProvider.Delays.afterBlockDestroy, cancellationToken: token);
            }

            stateMachine.EnterState<SpawnState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {
            blockDestroyService.OnDestroy -= CountTargets;
        }

        private void CountTargets(Block block)
        {
            winLoseService.TryDecreaseCount(block.Type);
        }
    }
}