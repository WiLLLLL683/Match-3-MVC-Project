using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using Model.Services;
using Utils;
using Model.Infrastructure.Commands;
using System.Collections;

namespace Infrastructure
{
    public class InputMoveBlockState : IPayLoadedState<(Vector2Int startPos, Directions direction)>
    {
        private readonly Game game;
        private readonly IStateMachine stateMachine;
        private readonly IBlockMatchService matchService;
        private readonly IBlockMoveService moveService;

        private GameBoard gameBoard;
        private Vector2Int startPos;
        private Directions direction;

        public InputMoveBlockState(Game game,
            IStateMachine stateMachine,
            IBlockMatchService matchService,
            IBlockMoveService moveService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.moveService = moveService;
        }

        public IEnumerator OnEnter((Vector2Int startPos, Directions direction) payLoad)
        {
            startPos = payLoad.startPos;
            direction = payLoad.direction;
            gameBoard = game.CurrentLevel.gameBoard;

            //бездействие при долгом зажатии блока на одном месте
            if (direction == Directions.Zero)
                yield break;

            MoveBlock();
        }

        public IEnumerator OnExit()
        {
            yield break;
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