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
    public class InputBoosterState : IPayLoadedState<IBooster>
    {
        private IStateMachine stateMachine;
        private IBoosterService boosterInventory;
        private readonly IWinLoseService winLoseService;
        private readonly ICounterTarget turnTarget;

        private IBooster booster;

        public InputBoosterState(IStateMachine stateMachine,
            IBoosterService boosterInventory,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.boosterInventory = boosterInventory;
            this.winLoseService = winLoseService;
            this.turnTarget = configProvider.Turn.CounterTarget;
        }

        public async UniTask OnEnter(IBooster payLoad, CancellationToken token)
        {
            //TODO использовать бустер
            HashSet<Cell> matches = null;

            winLoseService.DecreaseCountIfPossible(turnTarget);
            stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}