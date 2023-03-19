using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
{
    public class WaitState : IState
    {
        //зависимости
        private Game game;
        private StateMachine stateMachine;
        private IMatchSystem matchSystem;
        //данные
        private Level level; //TODO проверить меняется ли уровень при изменении в Game?
        private List<Cell> hintCells;


        public WaitState(Game _game, StateMachine _stateMachine, AllSystems _systems) //TODO input
        {
            game = _game;
            stateMachine = _stateMachine;
            matchSystem = _systems.GetSystem<IMatchSystem>();
        }

        public void OnStart()
        {
            level = game.Level;

            //проверка на проигрыш
            if (level.CheckLose())
                stateMachine.SetState<LoseState>();

            //проверка на выигрыш
            if (level.CheckWin())
                stateMachine.SetState<WinState>();

            //TODO подписка на ивенты инпута

            //поиск блоков для подсказки
            hintCells = matchSystem.FindFirstHint(); //TODO как прокинуть это во вью? через ивент?
        }

        public void OnEnd()
        {
            //отписка от ивента инпута
        }

        private void OnInputMove(Vector2Int _startPos, Directions _direction)
        {
            stateMachine.GetState<TurnState>().SetInput(_startPos, _direction);
            stateMachine.SetState<TurnState>();
        }

        private void OnInputBooster(IBooster _booster)
        {
            stateMachine.GetState<BoosterState>().SetInput(_booster);
            stateMachine.SetState<BoosterState>();
        }
    }
}