using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model.GameLogic
{
    public class LoadGameState : IState
    {
        private Game game;
        private StateMachine stateMachine;

        public LoadGameState(Game _game)
        {
            game = _game;
            stateMachine = _game.StateMachine;
        }

        public void OnStart()
        {
            //TODO загрузка сохранения
            stateMachine.ChangeState(new MetaGameState());
        }

        public void OnEnd()
        {

        }
    }
}