using Model.Objects;
using Model.Services;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class WaitState : AModelState
    {
        private readonly Game game;
        private readonly IStateMachine<AModelState> stateMachine;
        private readonly IMatchService matchService;
        private readonly IWinLoseService winLoseService;

        private Level level; //TODO проверить меняется ли уровень при изменении в Game?
        private HashSet<Cell> hintCells;


        public WaitState(Game game, IStateMachine<AModelState> stateMachine, IMatchService matchService, IWinLoseService winLoseService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.winLoseService = winLoseService;
        }

        public override void OnEnter()
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

        public override void OnExit()
        {

        }

        public override void OnInputMoveBlock(Vector2Int startPos, Directions direction)
        {
            stateMachine.GetState<TurnState>().SetInput(startPos, direction);
            stateMachine.EnterState<TurnState>();
        }

        public override void OnInputActivateBlock(Vector2Int startPos)
        {
            stateMachine.GetState<TurnState>().SetInput(startPos, Directions.Zero);
            stateMachine.EnterState<TurnState>();
        }

        public override void OnInputBooster(IBooster booster)
        {
            stateMachine.GetState<BoosterState>().SetInput(booster);
            stateMachine.EnterState<BoosterState>();
        }
    }
}