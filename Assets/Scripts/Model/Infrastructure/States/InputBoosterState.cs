using Model.Objects;
using Model.Services;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class InputBoosterState : IPayLoadedState<IBooster>
    {
        private Game game;
        private Level level;
        private IStateMachine stateMachine;
        private IBoosterService boosterInventory;

        private IBooster booster;

        public InputBoosterState(Game game, IStateMachine stateMachine, IBoosterService boosterInventory)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.boosterInventory = boosterInventory;
        }

        public void OnEnter(IBooster payLoad)
        {
            level = game.CurrentLevel;

            //TODO использовать бустер
            HashSet<Cell> matches = null;

            stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
        }

        public void OnEnter()
        {
            Debug.LogWarning("Payloaded states should not be entered without payload, returning to WaitState state");
            stateMachine.EnterState<WaitState>();
        }

        public void OnExit()
        {

        }
    }
}