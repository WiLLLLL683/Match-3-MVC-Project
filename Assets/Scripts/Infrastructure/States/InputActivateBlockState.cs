using Cysharp.Threading.Tasks;
using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт кор-игры для изменения модели в ответ на инпут(нажатие на блок)
    /// PayLoad(Vector2Int) - позиция нажатого блока
    /// </summary>
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

        public async UniTask OnEnter(Vector2Int payLoad, CancellationToken token)
        {
            gameBoard = game.CurrentLevel.gameBoard;
            PressBlock(payLoad);
        }

        public async UniTask OnExit(CancellationToken token)
        {

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