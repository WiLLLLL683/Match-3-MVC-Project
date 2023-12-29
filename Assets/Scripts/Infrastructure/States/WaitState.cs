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
        private readonly IConfigProvider configProvider;

        private HashSet<Cell> hintCells;

        public WaitState(IStateMachine stateMachine, IWinLoseService winLoseService, IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.winLoseService = winLoseService;
            this.configProvider = configProvider;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            //проверка на проигрыш
            if (winLoseService.CheckLose())
            {
                await UniTask.WaitForSeconds(configProvider.Delays.beforeLose, cancellationToken: token);
                stateMachine.EnterState<LoseState>();
                return;
            }

            //проверка на выигрыш
            if (winLoseService.CheckWin())
            {
                await UniTask.WaitForSeconds(configProvider.Delays.beforeWin, cancellationToken: token);
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