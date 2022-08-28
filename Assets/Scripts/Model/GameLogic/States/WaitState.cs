using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class WaitState : AState
    {
        public List<Cell> hintCells;

        private StateContext context;

        public WaitState(GameStateMachine _stateMachine, StateContext _context) : base(_stateMachine)
        {
            context = _context;
        }

        public override void OnStart()
        {
            base.OnStart();

            //проверка на проигрыш
            if (context.Level.CheckLose())
                stateMachine.ChangeState(new LoseState(stateMachine));

            //проверка на выигрыш
            if (context.Level.CheckWin())
                stateMachine.ChangeState(new WinState(stateMachine));

            //подписка на ивенты инпута
            stateMachine.eventDispatcher.OnInput += OnInput;

            //поиск блоков для подсказки
            hintCells = context.MatchSystem.FindFirstHint(); //TODO как прокинуть это во вью? через ивент?

        }

        public override void OnEnd()
        {
            base.OnEnd();

            //TODO отписка на ивент инпута
        }

        private void OnInput(Vector2Int _startPos, Directions _direction)
        {
            stateMachine.ChangeState(new TurnState(stateMachine, context, _startPos, _direction));
        }

        private void OnInputBooster()
        {
            //TODO booster
        }
    }
}