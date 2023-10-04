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

        public Game(LevelSO[] allLevels, int currentLevelIndex, ICellType invisibleCellType)
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
            var patternFactory = new PatternFactory();
            var hintPatternFactory = new HintPatternFactory();
            var counterFactory = new CounterFactory();
            var levelFactory = new LevelFactory(gameboardFactory, balanceFactory, patternFactory, hintPatternFactory, counterFactory);

            //сервисы
            var validationService = new ValidationService();
            var blockSpawnService = new BlockSpawnService(blockFactory, validationService);
            var matchService = new MatchService(validationService);
            var gravityService = new GravityService(validationService);
            var moveService = new BlockMoveService(validationService);
            var blockDestroyService = new BlockDestroyService(validationService, blockFactory);

            stateMachine = new();
            stateMachine.AddState(new LoadLevelState(this, stateMachine, levelFactory, blockSpawnService, validationService, matchService));
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

        /// <summary>
        /// ќбновить данные об уровне
        /// </summary>
        public void SetLevel(Level level)
        {
            CurrentLevel = level;
        }

        public void MoveBlock(Vector2Int blockPosition, Directions direction) => stateMachine.CurrentState.OnInputMoveBlock(blockPosition, direction);
        
        public void ActivateBooster(IBooster_Readonly booster) => stateMachine.CurrentState.OnInputBooster((IBooster)booster); //TODO нужен более надежный способ получени€ конкретного типа бустера, например id
       
        public void ActivateBlock(Vector2Int blockPosition) => stateMachine.CurrentState.OnInputActivateBlock(blockPosition);
    }
}