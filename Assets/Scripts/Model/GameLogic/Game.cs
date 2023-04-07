using Data;
using Model.Objects;
using Model.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class Game
    {
        public Level Level { get; private set; }
        public SpawnSystem SpawnSystem { get; private set; }
        public MatchSystem MatchSystem { get; private set; }
        public GravitySystem GravitySystem { get; private set; }
        public MoveSystem MoveSystem { get; private set; }
        public CurrencyInventory CurrencyInventory { get; private set; }
        public BoosterInventory BoosterInventory { get; private set; }
        public GameStateMachine StateMachine { get; private set; }
        public EventDispatcher EventDispatcher { get; private set; }

        public Game()
        {
            CurrencyInventory = new CurrencyInventory();
            BoosterInventory = new BoosterInventory();
            EventDispatcher = new EventDispatcher();
            StateMachine = new GameStateMachine(EventDispatcher);
        }

        public void SetLevel(Level _level)
        {
            Level = _level;
            SpawnSystem = new SpawnSystem(Level);
            MatchSystem = new MatchSystem(Level);
            GravitySystem = new GravitySystem(Level.gameBoard);
            MoveSystem = new MoveSystem(Level.gameBoard);
        }

        public void StartMetaGame()
        {
            //������ ����-����
            StateMachine.ChangeState(new MetaGameState());
        }

        public void StartCoreGame(LevelData levelData)
        {
            //������ ���-����
            StateMachine.ChangeState(new LoadLevelState(this, levelData));
        }
    }
}