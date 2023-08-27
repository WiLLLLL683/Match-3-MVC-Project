using Data;
using Model.Factories;
using Model.Objects;
using Model.Readonly;
using Model.Services;
using Model.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    /// <summary>
    /// ќсновной объект модели игры
    /// </summary>
    public class Game : IGame
    {
        //meta game
        public LevelSelection LevelSelection { get; private set; }
        public CurrencyInventory CurrencyInventory { get; private set; }
        //core game
        public Level CurrentLevel { get; private set; }
        public BoosterInventory BoosterInventory { get; private set; }
        public PlayerSettings PlayerSettings { get; private set; }
        public string CurrentStateName => stateMachine?.CurrentState?.GetType().Name;

        private StateMachine<AModelState> stateMachine;
        private AllSystems systems;

        public Game(LevelConfig[] allLevels, int currentLevelIndex, ICellType invisibleCellType)
        {
            CurrencyInventory = new CurrencyInventory();
            BoosterInventory = new BoosterInventory();
            LevelSelection = new LevelSelection(allLevels, currentLevelIndex);
            PlayerSettings = new(true, false); //TODO загрузка из сохранени€

            //фабрики
            var blockFactory = new BlockFactory();
            var cellFactory = new CellFactory(invisibleCellType);
            var gameboardFactory = new GameBoardFactory(cellFactory);
            var balanceFactory = new BalanceFactory();
            var levelFactory = new LevelFactory(gameboardFactory, balanceFactory);

            //сервисы
            var validationService = new ValidationService();
            var blockSpawnService = new BlockSpawnService(blockFactory, validationService);

            systems = new AllSystems();
            systems.AddSystem<ISpawnSystem>(new SpawnSystem());
            systems.AddSystem<IMatchSystem>(new MatchSystem(validationService));
            systems.AddSystem<IGravitySystem>(new GravitySystem(validationService));
            systems.AddSystem<IMoveSystem>(new MoveSystem(validationService));

            stateMachine = new();
            stateMachine.AddState(new LoadLevelState(this, stateMachine, systems, levelFactory, blockSpawnService, validationService));
            stateMachine.AddState(new WaitState(this, stateMachine, systems));
            stateMachine.AddState(new TurnState(this, stateMachine, systems));
            stateMachine.AddState(new BoosterState(this, stateMachine, systems, BoosterInventory));
            stateMachine.AddState(new SpawnState(this, stateMachine, systems, blockSpawnService));
            stateMachine.AddState(new LoseState(stateMachine, systems));
            stateMachine.AddState(new WinState(stateMachine, systems));
            stateMachine.AddState(new BonusState(stateMachine, systems));
            stateMachine.AddState(new ExitState(stateMachine, systems));
        }

        /// <summary>
        /// «апуск выбранного уровн€ кор-игры
        /// </summary>
        public void StartLevel(LevelConfig levelData)
        {
            stateMachine.GetState<LoadLevelState>().SetLevelData(levelData);
            stateMachine.SetState<LoadLevelState>();
        }
        /// <summary>
        /// ќбновить данные об уровне
        /// </summary>
        public void SetLevel(Level level) => CurrentLevel = level;

        public void MoveBlock(Vector2Int blockPosition, Directions direction) => stateMachine.CurrentState.OnInputMoveBlock(blockPosition, direction);
        public void ActivateBooster(IBooster_Readonly booster) => stateMachine.CurrentState.OnInputBooster((IBooster)booster); //TODO нужен более надежный способ получени€ конкретного типа бустера, например id
        public void ActivateBlock(Vector2Int blockPosition) => stateMachine.CurrentState.OnInputActivateBlock(blockPosition);
    }
}