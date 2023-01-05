using Data;
using Model.Objects;
using Model.Systems;
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
            //Level = _level;
            SpawnSystem = new SpawnSystem(Level);
            MatchSystem = new MatchSystem(Level);
            GravitySystem = new GravitySystem(Level.gameBoard);
            MoveSystem = new MoveSystem(Level.gameBoard);
            Inventory = new Inventory();
            EventDispatcher = new EventDispatcher();
            StateMachine = new GameStateMachine(EventDispatcher);
        }

        public void StartMetaGame()
        {
            //запуск мета-игры
            StateMachine.ChangeState(new MetaState(StateMachine));
        }

        public void StartCoreGame(LevelData levelData)
        {
            //запуск кор-игры
            StateMachine.ChangeState(new LoadState(StateMachine, levelData));
        }
    }
}