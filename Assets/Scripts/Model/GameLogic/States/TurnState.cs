using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class TurnState : ACoreGameState
    {
        private Vector2Int startPos;
        private Directions direction;
        //private Booster booster; //TODO booster

        public TurnState(GameStateMachine _stateMachine, StateContext _context, Vector2Int _startPos, Directions _direction) : base(_stateMachine,_context)
        {
            startPos = _startPos;
            direction = _direction;
        }

        public override void OnStart()
        {
            base.OnStart();

            if (direction == Directions.Zero)
            {
                PressBlock();
            }
            else
            {
                MoveBlock();
            }
        }

        private void MoveBlock()
        {
            //попытка хода
            IAction swapAction = context.MoveSystem.Move(startPos, direction);
            swapAction.Execute();

            //проверка на результативность хода
            List<Cell> matches = context.MatchSystem.FindMatches();
            if (matches.Count > 0)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    context.Level.UpdateGoals(matches[i].block.type);
                    matches[i].DestroyBlock();
                }
                SucsessfullTurn();
            }
            else
            {
                swapAction.Undo();
                stateMachine.PrevoiusState();
            }
        }

        private void PressBlock()
        {
            bool turnSucsess = context.Level.gameBoard.cells[startPos.x, startPos.y].block.Activate();

            if (turnSucsess)
            {
                SucsessfullTurn();
            }
            else
            {
                stateMachine.PrevoiusState();
            }
        }

        private void SucsessfullTurn()
        {
            //TODO засчитать ход в логгер
            //TODO обновить счетчики
            stateMachine.ChangeState(new SpawnState(stateMachine,context));
        }
    }
}