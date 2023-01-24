using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
{
    public class BoosterState : IState
    {
        private Game game;
        private Level level;
        private StateMachine stateMachine;

        private IBooster booster;

        public BoosterState(Game _game)
        {
            game = _game;
            stateMachine = _game.StateMachine;
        }

        public void SetInput(IBooster _booster)
        {
            booster = _booster;
        }

        public void OnStart()
        {
            level = game.Level;
            //TODO использовать бустер
            //TODO засчитать ход
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