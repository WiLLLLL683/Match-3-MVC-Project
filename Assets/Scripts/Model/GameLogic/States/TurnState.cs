using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class TurnState : AState
    {
        private StateContext context;
        private Vector2Int startPos;
        private Directions direction;

        public TurnState(GameStateMachine _stateMachine, StateContext _context, Vector2Int _startPos, Directions _direction) : base(_stateMachine)
        {
            context = _context;
            startPos = _startPos;
            direction = _direction;
        }

        public override void OnStart()
        {
            base.OnStart();

            //при нажатии на блок:
            if (direction == Directions.Zero)
            {
                bool turnSucsess = context.Level.gameBoard.cells[startPos.x, startPos.y].block.Activate();

                if (turnSucsess)
                {
                    SucsessfullTurn();
                    return;
                }
                else
                {
                    stateMachine.PrevoiusState();
                }
            }

            //при сдвиге блока:
            //попытка хода
            IAction swapAction = context.SwitchSystem.Switch(startPos, direction);
            swapAction.Execute();

            //проверка на результативность хода
            List<Cell> matches = context.MatchSystem.FindMatches();
            if (matches.Count > 0)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    matches[i].DestroyBlock();
                    //TODO обновить счетчики
                }
                SucsessfullTurn();
            }
            else
            {
                swapAction.Undo();
                stateMachine.PrevoiusState();
            }
        }

        private void SucsessfullTurn()
        {
            //TODO засчитать ход в логгер
            //TODO обновить счетчики
            stateMachine.ChangeState(new SpawnState(stateMachine));
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}