using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class WaitState : ACoreGameState
    {
        public List<Cell> hintCells;

        public WaitState(GameStateMachine _stateMachine, StateContext _contex) : base(_stateMachine, _contex) { }

        public override void OnStart()
        {
            base.OnStart();

            //проверка на проигрыш
            if (context.Level.CheckLose())
                stateMachine.ChangeState(new LoseState(stateMachine,context));

            //проверка на выигрыш
            if (context.Level.CheckWin())
                stateMachine.ChangeState(new WinState(stateMachine,context));

            //подписка на ивенты инпута
            stateMachine.eventDispatcher.OnInputMove += OnInputMove;
            stateMachine.eventDispatcher.OnInputBooster += OnInputBooster;

            //поиск блоков для подсказки
            hintCells = context.MatchSystem.FindFirstHint(); //TODO как прокинуть это во вью? через ивент?
        }

        public override void OnEnd()
        {
            base.OnEnd();

            //отписка от ивента инпута
            stateMachine.eventDispatcher.OnInputMove -= OnInputMove;
            stateMachine.eventDispatcher.OnInputBooster -= OnInputBooster;
        }

        private void OnInputMove(Vector2Int _startPos, Directions _direction)
        {
            stateMachine.ChangeState(new TurnState(stateMachine, context, _startPos, _direction));
        }

        private void OnInputBooster()
        {
            //TODO booster
        }
    }
}