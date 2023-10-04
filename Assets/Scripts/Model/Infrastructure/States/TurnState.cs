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
        private readonly IBlockMoveService blockMoveService;
        private readonly IBlockDestroyService blockDestroyService;

        private GameBoard gameBoard;
        private Vector2Int startPos;
        private Directions direction;

        public TurnState(Game game, StateMachine<AModelState> stateMachine, IMatchService matchService, IBlockMoveService moveService, IBlockDestroyService blockDestroyService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.blockMoveService = moveService;
            this.blockDestroyService = blockDestroyService;
        }

        public void SetInput(Vector2Int startPos, Directions direction)
        {
            this.startPos = startPos;
            this.direction = direction;
        }

        public override void OnStart()
        {
            gameBoard = game.CurrentLevel.gameBoard;

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
            IAction swapAction = blockMoveService.Move(gameBoard, startPos, direction);

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
            bool turnSucsess = gameBoard.Cells[startPos.x, startPos.y].Block.Activate(); //TODO возвращать IAction

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
                blockDestroyService.Destroy(gameBoard, match);
            }
            stateMachine.SetState<SpawnState>();
        }
    }
}