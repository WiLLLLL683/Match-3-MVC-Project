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
        private IStateMachine stateMachine;
        private IBoosterService boosterService;
        private readonly IWinLoseService winLoseService;
        private readonly ICounterTarget turnTarget;

        private IBooster booster;

        public InputBoosterState(IStateMachine stateMachine,
            IBoosterService boosterService,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.boosterService = boosterService;
            this.winLoseService = winLoseService;
            this.turnTarget = configProvider.Turn.CounterTarget;
        }

        public async UniTask OnEnter((int id, Vector2Int startPosition) payLoad, CancellationToken token)
        {
            HashSet<Cell> cells = boosterService.UseBooster(payLoad.id, payLoad.startPosition);
            stateMachine.EnterState<DestroyState, HashSet<Cell>>(cells);
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}