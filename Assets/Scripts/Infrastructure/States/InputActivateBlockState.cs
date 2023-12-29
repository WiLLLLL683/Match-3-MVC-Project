using Config;
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
        private readonly IWinLoseService winLoseService;
        private readonly ICounterTarget turnTarget;

        private GameBoard gameBoard;

        public InputActivateBlockState(Game game,
            IStateMachine stateMachine,
            IBlockMatchService matchService,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.winLoseService = winLoseService;
            this.turnTarget = configProvider.Turn.CounterTarget;
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
                winLoseService.DecreaseCountIfPossible(turnTarget);
                stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
            }
            else
            {
                stateMachine.EnterState<WaitState>(); //Возврат к ожиданию инпута
            }
        }
    }
}