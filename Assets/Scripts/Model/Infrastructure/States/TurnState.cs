using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Model.Services;
using Utils;

namespace Model.Infrastructure
{
    public class TurnState : AModelState
    {
        private readonly Game game;
        private readonly StateMachine<AModelState> stateMachine;
        private readonly IMatchService matchService;
        private readonly IMoveService moveService;

        private Level level;
        private Vector2Int startPos;
        private Directions direction;

        public TurnState(Game game, StateMachine<AModelState> stateMachine, IMatchService matchService, IMoveService moveService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.moveService = moveService;
        }

        public void SetInput(Vector2Int startPos, Directions direction)
        {
            this.startPos = startPos;
            this.direction = direction;
        }

        public override void OnStart()
        {
            level = game.CurrentLevel;

            if (direction == Directions.Zero)
            {
                PressBlock();
            }
            else
            {
                MoveBlock();
            }
        }

        public override void OnEnd()
        {

        }

        private void MoveBlock()
        {
            IAction swapAction = moveService.Move(level.gameBoard, startPos, direction);
            swapAction?.Execute();

            HashSet<Cell> matches = matchService.FindAllMatches();

            if (matches.Count > 0)
            {
                SucsessfullTurn(matches);
            }
            else
            {
                swapAction?.Undo();
                stateMachine.SetPreviousState();
            }
        }

        private void PressBlock()
        {
            bool turnSucsess = level.gameBoard.Cells[startPos.x, startPos.y].Block.Activate(); //TODO возвращать IAction

            HashSet<Cell> matches = matchService.FindAllMatches();

            if (turnSucsess)
            {
                SucsessfullTurn(matches);
            }
            else
            {
                stateMachine.SetPreviousState();
            }
        }

        private void SucsessfullTurn(HashSet<Cell> matches)
        {
            //TODO засчитать ход в логгер
            //TODO обновить счетчики
            foreach (Cell match in matches)
            {
                //level.UpdateGoals(matches[i].Block.Type);
                match.Block.Destroy();
            }
            stateMachine.SetState<SpawnState>();
        }
    }
}