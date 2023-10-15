using System;
using UnityEngine;
using Config;
using Model.Factories;
using Model.Objects;
using Model.Readonly;
using Model.Services;
using Utils;

namespace Model.Infrastructure
{
    /// <summary>
    /// ќсновной объект модели игры
    /// </summary>
    [Serializable]
    public class Game : IGame
    {
        //meta game
        public LevelProgress LevelProgress;
        public PlayerSettings PlayerSettings;

        //core game
        public Level CurrentLevel;
        public string CurrentStateName => stateMachine?.CurrentState?.GetType().Name;

        //factories
        private readonly IBlockFactory blockFactory;
        private readonly ICellFactory cellFactory;
        private readonly IGameBoardFactory gameboardFactory;
        private readonly IMatchPatternFactory matchPatternFactory;
        private readonly IHintPatternFactory hintPatternFactory;
        private readonly ICounterFactory counterFactory;
        private readonly ILevelFactory levelFactory;

        //services
        public readonly ICurrencyService CurrencyInventory;
        public readonly IBoosterService BoosterService;
        private readonly IValidationService validationService;
        private readonly IRandomBlockTypeService randomService;
        private readonly IBlockSpawnService spawnService;
        private readonly IMatchService matchService;
        private readonly IGravityService gravityService;
        private readonly IBlockMoveService moveService;
        private readonly IBlockDestroyService destroyService;
        private readonly IWinLoseService winLoseService;

        private readonly StateMachine<AModelState> stateMachine = new();

        public Game(CellTypeSetSO allCellTypes, int maxLevelIndex)
        {
            LevelProgress = new LevelProgress(maxLevelIndex);
            PlayerSettings = new(true, false); //TODO загрузка из сохранени€

            //factories
            blockFactory = new BlockFactory();
            cellFactory = new CellFactory(allCellTypes.invisibleCellType.type);
            gameboardFactory = new GameBoardFactory(cellFactory, allCellTypes);
            hintPatternFactory = new HintPatternFactory();
            matchPatternFactory = new MatchPatternFactory(hintPatternFactory);
            counterFactory = new CounterFactory();
            levelFactory = new LevelFactory(gameboardFactory, matchPatternFactory, counterFactory);

            //services
            CurrencyInventory = new CurrencyService();
            BoosterService = new BoosterService();
            validationService = new ValidationService();
            randomService = new RandomBlockTypeService();
            spawnService = new BlockSpawnService(blockFactory, validationService, randomService);
            matchService = new MatchService(validationService);
            gravityService = new GravityService(validationService);
            moveService = new BlockMoveService(validationService);
            destroyService = new BlockDestroyService(validationService, blockFactory);
            winLoseService = new WinLoseService();

            //game states
            stateMachine.AddState(new LoadLevelState(this, stateMachine, levelFactory, validationService, randomService, spawnService, matchService, gravityService, moveService, destroyService, winLoseService));
            stateMachine.AddState(new WaitState(this, stateMachine, matchService, winLoseService));
            stateMachine.AddState(new TurnState(this, stateMachine, matchService, moveService, destroyService));
            stateMachine.AddState(new BoosterState(this, stateMachine, BoosterService));
            stateMachine.AddState(new SpawnState(this, stateMachine, spawnService, matchService, gravityService, destroyService));
            stateMachine.AddState(new LoseState(stateMachine));
            stateMachine.AddState(new WinState(stateMachine));
            stateMachine.AddState(new BonusState(stateMachine));
            stateMachine.AddState(new ExitState(stateMachine));
        }

        /// <summary>
        /// «апуск выбранного уровн€ кор-игры
        /// </summary>
        public void StartLevel(LevelSO levelData)
        {
            stateMachine.GetState<LoadLevelState>().SetLevelData(levelData);
            stateMachine.SetState<LoadLevelState>();
        }

        public void MoveBlock(Vector2Int blockPosition, Directions direction) =>
            stateMachine.CurrentState.OnInputMoveBlock(blockPosition, direction);

        public void ActivateBooster(IBooster_Readonly booster) =>
            stateMachine.CurrentState.OnInputBooster((IBooster)booster); //TODO нужен более надежный способ получени€ конкретного типа бустера, например id

        public void ActivateBlock(Vector2Int blockPosition) =>
            stateMachine.CurrentState.OnInputActivateBlock(blockPosition);
    }
}