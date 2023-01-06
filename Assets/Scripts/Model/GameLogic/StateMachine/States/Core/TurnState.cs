using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class TurnState : IState
    {
        private Game game;
        private GameStateMachine stateMachine;
        private MoveSystem moveSystem;
        private MatchSystem matchSystem;
        private Level level;

        private Vector2Int startPos;
        private Directions direction;
        //private Booster booster; //TODO booster

        public TurnState(Game _game, Vector2Int _startPos, Directions _direction)
        {
            game = _game;
            stateMachine = _game.StateMachine;
            moveSystem = _game.MoveSystem;
            matchSystem = _game.MatchSystem;
            level = _game.Level;
            startPos = _startPos;
            direction = _direction;
        }

        public void OnStart()
        {
            if (direction == Directions.Zero)
            {
                PressBlock();
            }
            else
            {
                MoveBlock();
            }
        }

        public void OnEnd()
        {

        }

        private void MoveBlock()
        {
            //попытка хода
            IAction swapAction = moveSystem.Move(startPos, direction);
            swapAction.Execute();

            //проверка на результативность хода
            List<Cell> matches = matchSystem.FindMatches();
            if (matches.Count > 0)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    level.UpdateGoals(matches[i].block.type);
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
            bool turnSucsess = level.gameBoard.cells[startPos.x, startPos.y].block.Activate();

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
            stateMachine.ChangeState(new SpawnState(game));
        }
    }
}