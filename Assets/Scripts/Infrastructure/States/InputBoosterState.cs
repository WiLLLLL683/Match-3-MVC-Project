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
    public class InputBoosterState : IPayLoadedState<IBooster>
    {
        private Game game;
        private IStateMachine stateMachine;
        private IBoosterService boosterInventory;

        private IBooster booster;

        public InputBoosterState(Game game, IStateMachine stateMachine, IBoosterService boosterInventory)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.boosterInventory = boosterInventory;
        }

        public IEnumerator OnEnter(IBooster payLoad)
        {
            //TODO использовать бустер
            HashSet<Cell> matches = null;

            stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }
    }
}