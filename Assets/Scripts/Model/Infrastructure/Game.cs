using Config;
using Model.Factories;
using Model.Objects;
using Model.Readonly;
using Model.Services;
using Model.Systems;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    /// <summary>
    /// �������� ������ ������ ����
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

        public Game(LevelSO[] allLevels, int currentLevelIndex, ICellType invisibleCellType)
        {
            CurrencyInventory = new CurrencyInventory();
            BoosterInventory = new BoosterInventory();
            LevelSelection = new LevelSelection(allLevels, currentLevelIndex);
            PlayerSettings = new(true, false); //TODO �������� �� ����������

            //�������
            var blockFactory = new BlockFactory();
            var cellFactory = new CellFactory(invisibleCellType);
            var gameboardFactory = new GameBoardFactory(cellFactory);
            var balanceFactory = new BalanceFactory();
            var patternFactory = new PatternFactory();
            var hintPatternFactory = new HintPatternFactory();
            var levelFactory = new LevelFactory(gameboardFactory, balanceFactory, patternFactory, hintPatternFactory);

            //�������
            var validationService = new ValidationService();
            var blockSpawnService = new BlockSpawnService(blockFactory, validationService);
            var matchService = new MatchService(validationService);
            var gravityService = new GravityService(validationService);

            systems = new AllSystems();
            systems.AddSystem<IMoveSystem>(new MoveSystem(validationService));

            stateMachine = new();
            stateMachine.AddState(new LoadLevelState(this, stateMachine, levelFactory, blockSpawnService, validationService, matchService));
            stateMachine.AddState(new WaitState(this, stateMachine, matchService));
            stateMachine.AddState(new TurnState(this, stateMachine, systems, matchService));
            stateMachine.AddState(new BoosterState(this, stateMachine, systems, BoosterInventory));
            stateMachine.AddState(new SpawnState(this, stateMachine, systems, blockSpawnService, matchService, gravityService));
            stateMachine.AddState(new LoseState(stateMachine, systems));
            stateMachine.AddState(new WinState(stateMachine, systems));
            stateMachine.AddState(new BonusState(stateMachine, systems));
            stateMachine.AddState(new ExitState(stateMachine, systems));
        }

        /// <summary>
        /// ������ ���������� ������ ���-����
        /// </summary>
        public void StartLevel(LevelSO levelData)
        {
            stateMachine.GetState<LoadLevelState>().SetLevelData(levelData);
            stateMachine.SetState<LoadLevelState>();
        }

        /// <summary>
        /// �������� ������ �� ������
        /// </summary>
        public void SetLevel(Level level)
        {
            CurrentLevel = level;
            systems.SetLevel(level);
        }

        public void MoveBlock(Vector2Int blockPosition, Directions direction) => stateMachine.CurrentState.OnInputMoveBlock(blockPosition, direction);
        
        public void ActivateBooster(IBooster_Readonly booster) => stateMachine.CurrentState.OnInputBooster((IBooster)booster); //TODO ����� ����� �������� ������ ��������� ����������� ���� �������, �������� id
       
        public void ActivateBlock(Vector2Int blockPosition) => stateMachine.CurrentState.OnInputActivateBlock(blockPosition);
    }
}