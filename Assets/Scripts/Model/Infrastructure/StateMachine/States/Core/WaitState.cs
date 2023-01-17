using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class WaitState : IState
    {
        private Game game;
        private StateMachine stateMachine;
        private IMatchSystem matchSystem;
        private Level level;
        private List<Cell> hintCells;
        private EventDispatcher eventDispatcher;


        public WaitState(Game _game)
        {
            game = _game;
            level = _game.Level;
            eventDispatcher = _game.EventDispatcher;
            matchSystem = _game.Systems.MatchSystem;
            stateMachine = _game.StateMachine;
        }

        public void OnStart()
        {
            //проверка на проигрыш
            if (level.CheckLose())
                stateMachine.ChangeState(new LoseState());

            //проверка на выигрыш
            if (level.CheckWin())
                stateMachine.ChangeState(new WinState());

            //подписка на ивенты инпута
            eventDispatcher.OnInputMove += OnInputMove;
            eventDispatcher.OnInputBooster += OnInputBooster;

            //поиск блоков для подсказки
            hintCells = matchSystem.FindFirstHint(); //TODO как прокинуть это во вью? через ивент?
        }

        public void OnEnd()
        {
            //отписка от ивента инпута
            eventDispatcher.OnInputMove -= OnInputMove;
            eventDispatcher.OnInputBooster -= OnInputBooster;
        }

        private void OnInputMove(Vector2Int _startPos, Directions _direction)
        {
            stateMachine.ChangeState(new TurnState(game, _startPos, _direction));
        }

        private void OnInputBooster()
        {
            //TODO booster
        }
    }
}