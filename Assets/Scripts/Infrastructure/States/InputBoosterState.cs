using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт кор-игры для изменения модели в ответ на инпут(использование бустера)
    /// PayLoad(IBooster) - выбранный бустер
    /// </summary>
    public class InputBoosterState : PayLoadedStateBase<IBooster>
    {
        private IStateMachine stateMachine;
        private IBoosterService boosterInventory;

        private IBooster booster;

        public InputBoosterState(IStateMachine stateMachine, IBoosterService boosterInventory)
        {
            this.stateMachine = stateMachine;
            this.boosterInventory = boosterInventory;
        }

        public override IEnumerator OnEnter(IBooster payLoad)
        {
            //TODO использовать бустер
            HashSet<Cell> matches = null;

            stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
            yield break;
        }
    }
}