using Data;
using Model.Objects;
using Model.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
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
            Systems = new AllSystems();
            StateMachine = new StateMachine(
                new Dictionary<Type, IState>
                {
                    [typeof(LoadGameState)] = new LoadGameState(this),
                    [typeof(MetaGameState)] = new MetaGameState(),
                    [typeof(BonusState)] = new BonusState(),
                    [typeof(ExitState)] = new ExitState(),
                    [typeof(HintState)] = new HintState(),
                    [typeof(MetaGameState)] = new MetaGameState(),
                    [typeof(LoadLevelState)] = new LoadLevelState(this, new LevelData()), //TODO нужно прокидывать нужный уровень в момент загрузки кор-игры
                    [typeof(LoseState)] = new LoseState(),
                    [typeof(SpawnState)] = new SpawnState(this),
                    [typeof(TurnState)] = new TurnState(this, new Vector2Int(), Directions.Up), //TODO нужно прокидывать инпут в момент хода
                    [typeof(WaitState)] = new WaitState(this),
                    [typeof(WinState)] = new WinState()
                });
        }

        public void SetLevel(Level _level)
        {
            Level = _level;
            Systems.UpdateSystems(_level);
        }

        public void StartMetaGame() => StateMachine.SetState<MetaGameState>();

        public void StartCoreGame(LevelData levelData) => StateMachine.SetState<LoadLevelState>();//new LoadLevelState(this, levelData));
    }
}