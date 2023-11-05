﻿using Model.Objects;
using Model.Services;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class WaitState : IState
    {
        private readonly Game game;
        private readonly IStateMachine stateMachine;
        private readonly IMatchService matchService;
        private readonly IWinLoseService winLoseService;

        private Level level;
        private HashSet<Cell> hintCells;

        public WaitState(Game game, IStateMachine stateMachine, IMatchService matchService, IWinLoseService winLoseService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.winLoseService = winLoseService;
        }

        public void OnEnter()
        {
            level = game.CurrentLevel;

            //проверка на проигрыш
            if (winLoseService.CheckLose())
                stateMachine.EnterState<LoseState>();

            //проверка на выигрыш
            if (winLoseService.CheckWin())
                stateMachine.EnterState<WinState>();

            //поиск блоков для подсказки
            //hintCells = matchSystem.FindFirstHint(); //TODO как прокинуть это во вью? через ивент?
        }

        public void OnExit()
        {

        }
    }
}