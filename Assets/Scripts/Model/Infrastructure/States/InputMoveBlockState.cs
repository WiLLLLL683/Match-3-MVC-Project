using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Model.Services;
using Utils;
using Model.Infrastructure.Commands;

namespace Model.Infrastructure
{
    public class InputMoveBlockState : IPayLoadedState<(Vector2Int startPos, Directions direction)>
    {
        private readonly Game game;
        private readonly IStateMachine stateMachine;
        private readonly IMatchService matchService;
        private readonly IBlockMoveService moveService;

        private GameBoard gameBoard;
        private Vector2Int startPos;
        private Directions direction;

        public InputMoveBlockState(Game game,
            IStateMachine stateMachine,
            IMatchService matchService,
            IBlockMoveService moveService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.moveService = moveService;
        }

        public void OnEnter((Vector2Int startPos, Directions direction) payLoad)
        {
            startPos = payLoad.startPos;
            direction = payLoad.direction;
            gameBoard = game.CurrentLevel.gameBoard;

            //бездействие при долгом зажатии блока на одном месте
            if (direction == Directions.Zero)
                return;

            MoveBlock();
        }

        public void OnEnter()
        {
            Debug.LogWarning("Payloaded states should not be entered without payload, returning to WaitState state");
            stateMachine.EnterState<WaitState>();
        }

        public void OnExit()
        {

        }

        private void MoveBlock()
        {
            ICommand moveAction = new BlockMoveCommand(startPos, startPos + direction.ToVector2Int(), moveService);
            moveAction.Execute();

            HashSet<Cell> matches = matchService.FindAllMatches();

            if (matches.Count > 0)
            {
                stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
            }
            else
            {
                moveAction?.Undo();
                stateMachine.EnterState<WaitState>(); //Возврат к ожиданию инпута
            }
        }
    }
}