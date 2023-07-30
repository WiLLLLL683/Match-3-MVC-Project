using Data;
using Model.Objects;
using Model.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    /// <summary>
    /// Основной объект модели игры
    /// </summary>
    public class Game
    {
        //meta game
        public LevelSelection LevelSelection { get; private set; }
        public CurrencyInventory CurrencyInventory { get; private set; }
        //core game
        public Level Level { get; private set; }
        public BoosterInventory BoosterInventory { get; private set; }
        public PlayerSettings PlayerSettings { get; private set; }

        private StateMachine stateMachine;
        private AllSystems systems;

        public Game(LevelData[] allLevels, int currentLevelIndex)
        {
            CurrencyInventory = new CurrencyInventory();
            BoosterInventory = new BoosterInventory();
            LevelSelection = new LevelSelection(allLevels, currentLevelIndex);
            PlayerSettings = new(true, false); //TODO загрузка из сохранения

            systems = new AllSystems();
            systems.AddSystem<ISpawnSystem>(new SpawnSystem());
            systems.AddSystem<IMatchSystem>(new MatchSystem());
            systems.AddSystem<IGravitySystem>(new GravitySystem());
            systems.AddSystem<IMoveSystem>(new MoveSystem());

            stateMachine = new StateMachine();
            stateMachine.AddState(new LoadGameState(this, stateMachine));
            stateMachine.AddState(new MetaGameState());
            stateMachine.AddState(new LoadLevelState(this, stateMachine, systems));
            stateMachine.AddState(new WaitState(this, stateMachine, systems)); //TODO нужна ссылка на инпут/контроллер, для подписки на события
            stateMachine.AddState(new TurnState(this, stateMachine, systems));
            stateMachine.AddState(new BoosterState(this, stateMachine, systems, BoosterInventory));
            stateMachine.AddState(new SpawnState(this, stateMachine, systems));
            stateMachine.AddState(new HintState(stateMachine, systems));
            stateMachine.AddState(new LoseState(stateMachine, systems));
            stateMachine.AddState(new WinState(stateMachine, systems));
            stateMachine.AddState(new BonusState(stateMachine, systems));
            stateMachine.AddState(new ExitState(stateMachine, systems));
        }

        /// <summary>
        /// Загрузка игры
        /// </summary>
        public void LoadGame() => stateMachine.SetState<LoadGameState>();
        /// <summary>
        /// Запуск мета-игры
        /// </summary>
        public void StartMetaGame() => stateMachine.SetState<MetaGameState>();
        /// <summary>
        /// Запуск выбранного уровня кор-игры
        /// </summary>
        public void StartCoreGame(LevelData levelData)
        {
            stateMachine.GetState<LoadLevelState>().SetLevelData(levelData);
            stateMachine.SetState<LoadLevelState>();
        }
        /// <summary>
        /// Обновить данные об уровне
        /// </summary>
        public void SetLevel(Level _level) => Level = _level;
    }
}