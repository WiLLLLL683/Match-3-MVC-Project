using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class WaitState : AModelState
    {
        //зависимости
        private Game game;
        private StateMachine<AModelState> stateMachine;
        private IMatchSystem matchSystem;
        //данные
        private Level level; //TODO проверить меняется ли уровень при изменении в Game?
        private List<Cell> hintCells;


        public WaitState(Game _game, StateMachine<AModelState> _stateMachine, AllSystems _systems) //TODO input
        {
            game = _game;
            stateMachine = _stateMachine;
            matchSystem = _systems.GetSystem<IMatchSystem>();
        }

        public override void OnStart()
        {
            level = game.CurrentLevel;

            //проверка на проигрыш
            if (level.CheckLose())
                stateMachine.SetState<LoseState>();

            //проверка на выигрыш
            if (level.CheckWin())
                stateMachine.SetState<WinState>();

            //поиск блоков для подсказки
            hintCells = matchSystem.FindFirstHint(); //TODO как прокинуть это во вью? через ивент?
        }

        public override void OnEnd()
        {

        }

        public override void OnInputMoveBlock(Vector2Int startPos, Directions direction)
        {
            stateMachine.GetState<TurnState>().SetInput(startPos, direction);
            stateMachine.SetState<TurnState>();
        }

        public override void OnInputActivateBlock(Vector2Int startPos)
        {
            stateMachine.GetState<TurnState>().SetInput(startPos, Directions.Zero);
            stateMachine.SetState<TurnState>();
        }

        public override void OnInputBooster(IBooster booster)
        {
            stateMachine.GetState<BoosterState>().SetInput(booster);
            stateMachine.SetState<BoosterState>();
        }
    }
}