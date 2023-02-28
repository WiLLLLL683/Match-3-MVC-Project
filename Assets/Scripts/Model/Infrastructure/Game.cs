using Data;
using Model.Objects;
using Model.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
{
    /// <summary>
    /// �������� ������ ������ ����
    /// </summary>
    public class Game
    {
        public Level Level { get; private set; }
        public CurrencyInventory CurrencyInventory { get; private set; }
        public BoosterInventory BoosterInventory { get; private set; }
        public StateMachine StateMachine { get; private set; }
        //public EventDispatcher EventDispatcher { get; private set; }
        public AllSystems Systems { get; private set; }

        public Game()
        {
            CurrencyInventory = new CurrencyInventory();
            BoosterInventory = new BoosterInventory();
            //EventDispatcher = new EventDispatcher();

            Systems = new AllSystems();
            Systems.AddSystem(new SpawnSystem());
            Systems.AddSystem(new MatchSystem());
            Systems.AddSystem(new GravitySystem());
            Systems.AddSystem(new MoveSystem());

            StateMachine = new StateMachine();
            StateMachine.AddState(new LoadGameState(this));
            StateMachine.AddState(new MetaGameState());
            StateMachine.AddState(new LoadLevelState(this));
            StateMachine.AddState(new WaitState(this)); //TODO ����� ������ �� �����/����������, ��� �������� �� �������
            StateMachine.AddState(new TurnState(this));
            StateMachine.AddState(new BoosterState(this));
            StateMachine.AddState(new SpawnState(this));
            StateMachine.AddState(new HintState());
            StateMachine.AddState(new LoseState());
            StateMachine.AddState(new WinState());
            StateMachine.AddState(new BonusState());
            StateMachine.AddState(new ExitState());
        }

        /// <summary>
        /// �������� ����
        /// </summary>
        public void LoadGame() => StateMachine.SetState<LoadGameState>();
        /// <summary>
        /// ������ ����-����
        /// </summary>
        public void StartMetaGame() => StateMachine.SetState<MetaGameState>();
        /// <summary>
        /// ������ ���������� ������ ���-����
        /// </summary>
        /// <param name="levelData"></param>
        public void StartCoreGame(LevelData levelData)
        {
            StateMachine.GetState<LoadLevelState>().SetLevelData(levelData);
            StateMachine.SetState<LoadLevelState>();
        }
        /// <summary>
        /// �������� ������ �� ������
        /// </summary>
        public void SetLevel(Level _level) => Level = _level;
    }
}