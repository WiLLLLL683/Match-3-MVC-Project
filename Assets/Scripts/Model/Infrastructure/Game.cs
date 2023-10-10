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
        public LevelSelection LevelSelection;
        public CurrencyInventory CurrencyInventory;

        //core game
        public Level CurrentLevel;
        public BoosterInventory BoosterInventory;
        public PlayerSettings PlayerSettings;
        public string CurrentStateName => stateMachine?.CurrentState?.GetType().Name;

        private readonly IBlockFactory blockFactory;
        private readonly ICellFactory cellFactory;
        private readonly IGameBoardFactory gameboardFactory;
        private readonly IPatternFactory patternFactory;
        private readonly IHintPatternFactory hintPatternFactory;
        private readonly ICounterFactory counterFactory;
        private readonly ILevelFactory levelFactory;

        private readonly IValidationService validationService;
        private readonly IRandomBlockTypeService randomBlockTypeService;
        private readonly IBlockSpawnService blockSpawnService;
        private readonly IMatchService matchService;
        private readonly IGravityService gravityService;
        private readonly IBlockMoveService moveService;
        private readonly IBlockDestroyService blockDestroyService;

        private readonly StateMachine<AModelState> stateMachine = new();

        public Game(LevelSO[] allLevels, int currentLevelIndex, CellTypeSetSO allCellTypes, CellType invisibleCellType)
        {
            CurrencyInventory = new CurrencyInventory();
            BoosterInventory = new BoosterInventory();
            LevelSelection = new LevelSelection(allLevels, currentLevelIndex);
            PlayerSettings = new(true, false); //TODO загрузка из сохранени€

            //factories
            blockFactory = new BlockFactory();
            cellFactory = new CellFactory(invisibleCellType);
            gameboardFactory = new GameBoardFactory(cellFactory, allCellTypes);
            patternFactory = new PatternFactory();
            hintPatternFactory = new HintPatternFactory();
            counterFactory = new CounterFactory();
            levelFactory = new LevelFactory(gameboardFactory, patternFactory, hintPatternFactory, counterFactory);

            //services
            validationService = new ValidationService();
            randomBlockTypeService = new RandomBlockTypeService();
            blockSpawnService = new BlockSpawnService(blockFactory, validationService, randomBlockTypeService);
            matchService = new MatchService(validationService);
            gravityService = new GravityService(validationService);
            moveService = new BlockMoveService(validationService);
            blockDestroyService = new BlockDestroyService(validationService, blockFactory);

            //game states
            stateMachine.AddState(new LoadLevelState(this, stateMachine, levelFactory, randomBlockTypeService, blockSpawnService, validationService, matchService));
            stateMachine.AddState(new WaitState(this, stateMachine, matchService));
            stateMachine.AddState(new TurnState(this, stateMachine, matchService, moveService, blockDestroyService));
            stateMachine.AddState(new BoosterState(this, stateMachine, BoosterInventory));
            stateMachine.AddState(new SpawnState(this, stateMachine, blockSpawnService, matchService, gravityService, blockDestroyService));
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

        public void MoveBlock(Vector2Int blockPosition, Directions direction) => stateMachine.CurrentState.OnInputMoveBlock(blockPosition, direction);

        public void ActivateBooster(IBooster_Readonly booster) => stateMachine.CurrentState.OnInputBooster((IBooster)booster); //TODO нужен более надежный способ получени€ конкретного типа бустера, например id

        public void ActivateBlock(Vector2Int blockPosition) => stateMachine.CurrentState.OnInputActivateBlock(blockPosition);
    }
}