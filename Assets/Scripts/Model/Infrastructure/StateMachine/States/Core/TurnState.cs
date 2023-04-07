using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
{
    public class TurnState : IState
    {
        private Game game;
        private Level level;
        private StateMachine stateMachine;
        private IMoveSystem moveSystem;
        private IMatchSystem matchSystem;

        private Vector2Int startPos;
        private Directions direction;

        public TurnState(Game _game, StateMachine _stateMachine, AllSystems _systems)
        {
            game = _game;
            stateMachine = _stateMachine;
            moveSystem = _systems.GetSystem<IMoveSystem>();
            matchSystem = _systems.GetSystem<IMatchSystem>();
        }

        public void SetInput(Vector2Int _startPos, Directions _direction)
        {
            startPos = _startPos;
            direction = _direction;
        }

        public void OnStart()
        {
            level = game.Level;

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
                    level.UpdateGoals(matches[i].Block.Type);
                    matches[i].DestroyBlock();
                }
                SucsessfullTurn();
            }
            else
            {
                swapAction.Undo();
                stateMachine.SetPrevoiusState();
            }
        }

        private void PressBlock()
        {
            bool turnSucsess = level.gameBoard.Cells[startPos.x, startPos.y].Block.Activate();

            if (turnSucsess)
            {
                SucsessfullTurn();
            }
            else
            {
                stateMachine.SetPrevoiusState();
            }
        }

        private void SucsessfullTurn()
        {
            //TODO засчитать ход в логгер
            //TODO обновить счетчики
            stateMachine.SetState<SpawnState>();
        }
    }
}