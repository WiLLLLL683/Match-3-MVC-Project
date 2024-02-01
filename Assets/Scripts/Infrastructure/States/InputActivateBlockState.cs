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
        private readonly IBlockDestroyService destroyService;
        private readonly ICounterTarget turnTarget;

        private GameBoard gameBoard;

        public InputActivateBlockState(Game game,
            IStateMachine stateMachine,
            IBlockMatchService matchService,
            IWinLoseService winLoseService,
            IBlockDestroyService destroyService,
            IConfigProvider configProvider)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.winLoseService = winLoseService;
            this.destroyService = destroyService;
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
            //TODO возвращать IAction
            bool turnSucsess = gameBoard.Cells[position.x, position.y].Block.Type.Activate(position, destroyService);

            if (!turnSucsess)
            {
                stateMachine.EnterState<WaitState>(); //Возврат к ожиданию инпута
                return;
            }

            foreach (Cell cell in matchService.FindAllMatches())
            {
                destroyService.MarkToDestroy(cell.Block.Position);
            }

            winLoseService.TryDecreaseCount(turnTarget);
            stateMachine.EnterState<DestroyState>();
        }
    }
}