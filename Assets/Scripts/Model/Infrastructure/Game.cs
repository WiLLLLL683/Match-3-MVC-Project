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
        public CurrencyInventory CurrencyInventory { get; private set; }
        public BoosterInventory BoosterInventory { get; private set; }
        public StateMachine StateMachine { get; private set; }
        public EventDispatcher EventDispatcher { get; private set; }
        public AllSystems Systems { get; private set; }

        public Game()
        {
            CurrencyInventory = new CurrencyInventory();
            BoosterInventory = new BoosterInventory();
            EventDispatcher = new EventDispatcher();
            StateMachine = new StateMachine();
            Systems = new AllSystems();
        }

        public void SetLevel(Level _level)
        {
            Level = _level;
            Systems.UpdateSystems(_level);
        }

        public void StartMetaGame() => StateMachine.ChangeState(new MetaGameState());

        public void StartCoreGame(LevelData levelData) => StateMachine.ChangeState(new LoadLevelState(this, levelData));
    }
}