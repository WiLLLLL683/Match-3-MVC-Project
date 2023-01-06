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
        public Inventory Inventory { get; private set; }
        public GameStateMachine StateMachine { get; private set; }
        public EventDispatcher EventDispatcher { get; private set; }

        public Game()
        {
            Inventory = new Inventory();
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
            //запуск мета-игры
            StateMachine.ChangeState(new MetaState());
        }

        public void StartCoreGame(LevelData levelData)
        {
            //запуск кор-игры
            StateMachine.ChangeState(new LoadCoreGameState(this, levelData));
        }
    }
}