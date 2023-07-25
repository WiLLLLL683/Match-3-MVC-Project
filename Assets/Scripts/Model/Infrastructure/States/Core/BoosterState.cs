using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class BoosterState : IState
    {
        private Game game;
        private Level level;
        private StateMachine stateMachine;
        private BoosterInventory boosterInventory;

        private IBooster booster;

        public BoosterState(Game _game, StateMachine _stateMachine, AllSystems _systems, BoosterInventory _boosterInventory)
        {
            game = _game;
            stateMachine = _stateMachine;
            boosterInventory = _boosterInventory;
        }

        public void SetInput(IBooster _booster)
        {
            booster = _booster;
        }

        public void OnStart()
        {
            level = game.Level;
            if (true)
            {

            }
            //TODO использовать бустер
            SucsessfullTurn();
        }

        public void OnEnd()
        {

        }



        private void SucsessfullTurn()
        {
            //TODO засчитать ход в логгер
            //TODO обновить счетчики
            stateMachine.SetState<SpawnState>();
        }
    }
}