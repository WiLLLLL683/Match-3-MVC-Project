using System;
using UnityEngine;
using Config;
using Model.Factories;
using Model.Objects;
using Utils;

namespace Model.Infrastructure
{
    /// <summary>
    /// Основной объект модели игры
    /// </summary>
    [Serializable]
    public class Game : IGame
    {
        //meta game
        public LevelProgress LevelProgress;
        public PlayerSettings PlayerSettings;
        public CurrencyInventory CurrencyInventory;

        //core game
        public Level CurrentLevel;
        public string CurrentStateName => stateMachine?.CurrentState?.GetType().Name;

        private readonly StateMachine<AModelState> stateMachine = new();
        private readonly StateFactory stateFactory;

        public Game(StateMachine<AModelState> stateMachine, StateFactory stateFactory, LevelProgress levelProgress, 
            PlayerSettings playerSettings, CurrencyInventory currencyInventory)
        {
            this.stateMachine = stateMachine;
            this.stateFactory = stateFactory;
            LevelProgress = levelProgress;
            PlayerSettings = playerSettings;
            CurrencyInventory = currencyInventory;
        }

        public void Init()
        {
            //game states
            stateMachine.AddState(stateFactory.Create<LoadLevelState>());
            stateMachine.AddState(stateFactory.Create<WaitState>());
            stateMachine.AddState(stateFactory.Create<TurnState>());
            stateMachine.AddState(stateFactory.Create<BoosterState>());
            stateMachine.AddState(stateFactory.Create<SpawnState>());
            stateMachine.AddState(stateFactory.Create<LoseState>());
            stateMachine.AddState(stateFactory.Create<WinState>());
            stateMachine.AddState(stateFactory.Create<BonusState>());
            stateMachine.AddState(stateFactory.Create<ExitState>());
        }

        /// <summary>
        /// Запуск выбранного уровня кор-игры
        /// </summary>
        public void StartLevel(LevelSO levelData)
        {
            stateMachine.GetState<LoadLevelState>().SetLevelData(levelData);
            stateMachine.SetState<LoadLevelState>();
        }

        public void MoveBlock(Vector2Int blockPosition, Directions direction) =>
            stateMachine.CurrentState.OnInputMoveBlock(blockPosition, direction);

        public void ActivateBooster(IBooster booster) =>
            stateMachine.CurrentState.OnInputBooster((IBooster)booster); //TODO нужен более надежный способ получения конкретного типа бустера, например id

        public void ActivateBlock(Vector2Int blockPosition) =>
            stateMachine.CurrentState.OnInputActivateBlock(blockPosition);
    }
}