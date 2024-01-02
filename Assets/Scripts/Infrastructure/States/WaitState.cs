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
    /// Стейт кор-игры для ожидания инпута.
    /// </summary>
    public class WaitState : IState
    {
        private readonly IStateMachine stateMachine;
        private readonly IWinLoseService winLoseService;

        private HashSet<Cell> hintCells;

        public WaitState(IStateMachine stateMachine, IWinLoseService winLoseService)
        {
            this.stateMachine = stateMachine;
            this.winLoseService = winLoseService;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            if (winLoseService.CheckLose())
            {
                stateMachine.EnterState<LoseState>();
                return;
            }

            if (winLoseService.CheckWin())
            {
                stateMachine.EnterState<WinState>();
                return;
            }

            //поиск блоков для подсказки
            //hintCells = matchSystem.FindFirstHint(); //TODO как прокинуть это во вью? через ивент?
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}