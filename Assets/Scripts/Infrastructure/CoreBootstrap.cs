using Config;
using log4net.Core;
using Model.Infrastructure;
using Model.Services;
using Presenter;
using UnityEngine;
using Utils;
using View;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// Точка входа для сцены кор-игры
    /// Запускает все презентеры на сцене
    /// </summary>
    public class CoreBootstrap : MonoBehaviour
    {
        [NaughtyAttributes.ShowNativeProperty()]
        public string CurrentStateName => stateMachine?.CurrentState?.GetType().Name; //For debug in inspector

        private IStateMachine stateMachine;
        private IStateFactory stateFactory;
        private LevelSO currentLevel;
        private IInput input;
        private IHudPresenter hud;
        private IGameBoardPresenter gameBoard;
        private IBoosterInventoryPresenter boosterInventory;
        private IPausePresenter pause;
        private IEndGamePresenter endGame;

        [Inject]
        public void Construct(IStateMachine stateMachine,
            IStateFactory stateFactory,
            LevelSO currentLevel,
            IInput input,
            IHudPresenter hud,
            IGameBoardPresenter gameBoard,
            IBoosterInventoryPresenter boosterInventory,
            IPausePresenter pause,
            IEndGamePresenter endGame)
        {
            this.stateMachine = stateMachine;
            this.stateFactory = stateFactory;
            this.currentLevel = currentLevel;
            this.input = input;
            this.hud = hud;
            this.gameBoard = gameBoard;
            this.boosterInventory = boosterInventory;
            this.pause = pause;
            this.endGame = endGame;
        }

        private void Start()
        {
            //game states
            stateMachine.AddState(stateFactory.Create<LoadLevelState>());
            stateMachine.AddState(stateFactory.Create<WaitState>());
            stateMachine.AddState(stateFactory.Create<InputMoveBlockState>());
            stateMachine.AddState(stateFactory.Create<InputActivateBlockState>());
            stateMachine.AddState(stateFactory.Create<InputBoosterState>());
            stateMachine.AddState(stateFactory.Create<DestroyState>());
            stateMachine.AddState(stateFactory.Create<SpawnState>());
            stateMachine.AddState(stateFactory.Create<LoseState>());
            stateMachine.AddState(stateFactory.Create<WinState>());
            stateMachine.AddState(stateFactory.Create<BonusState>());
            stateMachine.AddState(stateFactory.Create<ExitState>());

            stateMachine.EnterState<LoadLevelState, LevelSO>(currentLevel);

            input.Enable();
            hud.Enable();
            gameBoard.Enable();
            boosterInventory.Enable();
            pause.Enable();
            endGame.Enable();
        }

        private void OnDestroy()
        {
            //input?.Disable(); //MonoBehavior будет уничтожен при выгрузке сцены и не требует отключения
            hud?.Disable();
            gameBoard?.Disable();
            boosterInventory?.Disable();
            pause?.Disable();
            endGame.Disable();
        }
    }
}