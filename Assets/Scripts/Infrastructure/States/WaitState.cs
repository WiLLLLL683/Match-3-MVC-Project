using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
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

        public IEnumerator OnEnter()
        {
            //проверка на проигрыш
            if (winLoseService.CheckLose())
                stateMachine.EnterState<LoseState>();

            //проверка на выигрыш
            if (winLoseService.CheckWin())
                stateMachine.EnterState<WinState>();

            //поиск блоков для подсказки
            //hintCells = matchSystem.FindFirstHint(); //TODO как прокинуть это во вью? через ивент?
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }
    }
}