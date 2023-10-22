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
        public readonly IBlockChangeTypeService blockChangeTypeService;
        public readonly IBlockDestroyService blockDestroyService;
        public readonly IBlockMoveService blockMoveService;
        public readonly IBlockSpawnService blockSpawnService;
        public readonly IBoosterService boosterService;
        public readonly ICellChangeTypeService cellChangeTypeService;
        public readonly ICellSetBlockService cellSetBlockService;
        public readonly ICounterService counterService;
        public readonly ICurrencyService currencyInventory;
        public readonly IGravityService gravityService;
        public readonly IMatchService matchService;
        public readonly IRandomBlockTypeService randomService;
        public readonly IValidationService validationService;
        public readonly IWinLoseService winLoseService;

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
            currencyInventory = new CurrencyService();
            boosterService = new BoosterService();
            validationService = new ValidationService();
            randomService = new RandomBlockTypeService();
            cellSetBlockService = new CellSetBlockService();
            cellChangeTypeService = new CellChangeTypeService();
            blockChangeTypeService = new BlockChangeTypeService(validationService);
            blockSpawnService = new BlockSpawnService(blockFactory, validationService, randomService, blockChangeTypeService, cellSetBlockService);
            matchService = new MatchService(validationService);
            blockMoveService = new BlockMoveService(validationService, cellSetBlockService);
            gravityService = new GravityService(validationService, blockMoveService);
            blockDestroyService = new BlockDestroyService(validationService, cellSetBlockService);
            counterService = new CounterService();
            winLoseService = new WinLoseService(counterService);

            //game states
            stateMachine.AddState(new LoadLevelState(this, stateMachine, levelFactory, validationService, randomService, blockSpawnService, matchService, gravityService, blockMoveService, blockDestroyService, winLoseService));
            stateMachine.AddState(new WaitState(this, stateMachine, matchService, winLoseService));
            stateMachine.AddState(new TurnState(this, stateMachine, matchService, blockMoveService, blockDestroyService));
            stateMachine.AddState(new BoosterState(this, stateMachine, boosterService));
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

        public void MoveBlock(Vector2Int blockPosition, Directions direction) =>
            stateMachine.CurrentState.OnInputMoveBlock(blockPosition, direction);

        public void ActivateBooster(IBooster_Readonly booster) =>
            stateMachine.CurrentState.OnInputBooster((IBooster)booster); //TODO нужен более надежный способ получени€ конкретного типа бустера, например id

        public void ActivateBlock(Vector2Int blockPosition) =>
            stateMachine.CurrentState.OnInputActivateBlock(blockPosition);
    }
}