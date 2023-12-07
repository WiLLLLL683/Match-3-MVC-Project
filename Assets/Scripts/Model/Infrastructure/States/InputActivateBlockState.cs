using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class InputActivateBlockState : IPayLoadedState<Vector2Int>
    {
        private readonly Game game;
        private readonly IStateMachine stateMachine;
        private readonly IBlockMatchService matchService;

        private GameBoard gameBoard;

        public InputActivateBlockState(Game game, IStateMachine stateMachine, IBlockMatchService matchService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.matchService = matchService;
        }

        public IEnumerator OnEnter(Vector2Int payLoad)
        {
            gameBoard = game.CurrentLevel.gameBoard;
            PressBlock(payLoad);
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }

        private void PressBlock(Vector2Int position)
        {
            bool turnSucsess = gameBoard.Cells[position.x, position.y].Block.Type.Activate(); //TODO возвращать IAction

            HashSet<Cell> matches = matchService.FindAllMatches();

            if (turnSucsess)
            {
                stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
            }
            else
            {
                stateMachine.EnterState<WaitState>(); //Возврат к ожиданию инпута
            }
        }
    }
}