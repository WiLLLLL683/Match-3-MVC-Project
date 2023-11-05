using System;
using UnityEngine;
using Config;
using Model.Factories;
using Model.Objects;
using Utils;

namespace Model.Infrastructure
{
    /// <summary>
    /// �������� ������ ������ ����
    /// </summary>
    [Serializable]
    public class Game
    {
        //meta game
        public LevelProgress LevelProgress;
        public PlayerSettings PlayerSettings;
        public CurrencyInventory CurrencyInventory;

        //core game
        public Level CurrentLevel;
        public string CurrentStateName => stateMachine?.CurrentState?.GetType().Name; //For debug in inspector

        private readonly IStateMachine<AModelState> stateMachine;
        private readonly StateFactory stateFactory;

        public Game(IStateMachine<AModelState> stateMachine,
            StateFactory stateFactory,
            LevelProgress levelProgress,
            PlayerSettings playerSettings,
            CurrencyInventory currencyInventory)
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
    }
}