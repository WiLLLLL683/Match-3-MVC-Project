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
    /// Стейт кор-игры для изменения модели в ответ на инпут(использование бустера)
    /// PayLoad(IBooster) - выбранный бустер
    /// </summary>
    public class InputBoosterState : IPayLoadedState<(int id, Vector2Int startPosition)>
    {
        private readonly IStateMachine stateMachine;
        private readonly IBoosterService boosterService;
        private readonly IBlockDestroyService destroyService;
        private readonly IConfigProvider configProvider;

        public InputBoosterState(IStateMachine stateMachine,
            IBoosterService boosterService,
            IBlockDestroyService destroyService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.boosterService = boosterService;
            this.destroyService = destroyService;
            this.configProvider = configProvider;
        }

        public async UniTask OnEnter((int id, Vector2Int startPosition) payLoad, CancellationToken token)
        {
            await UniTask.WaitForSeconds(configProvider.Delays.beforeBoosterUse);

            boosterService.UseBooster(payLoad.id, payLoad.startPosition);

            stateMachine.EnterState<DestroyState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}