using Config;
using Model.Infrastructure;
using Model.Objects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Infrastructure
{
    public class CoreState : IState
    {
        private readonly Game model;
        private readonly GameStateMachine gameStateMachine;
        private readonly IConfigProvider configProvider;
        private readonly ICoroutineRunner coroutineRunner;
        private const string CORE_SCENE_NAME = "Core";

        private CoreDependencies core;

        public CoreState(Game model,
            GameStateMachine gameStateMachine,
            IConfigProvider configProvider,
            ICoroutineRunner coroutineRunner)
        {
            this.model = model;
            this.gameStateMachine = gameStateMachine;
            this.configProvider = configProvider;
            this.coroutineRunner = coroutineRunner;
        }

        public void OnEnter()
        {
            if (!ValidateSelectedLevel())
            {
                gameStateMachine.EnterState<MetaState>();
                return;
            }

            coroutineRunner.StartCoroutine(LoadCore());
        }

        public void OnExit()
        {
            DisablePresenters();
        }

        private IEnumerator LoadCore()
        {
            yield return SceneManager.LoadSceneAsync(CORE_SCENE_NAME, LoadSceneMode.Single);
            GetSceneDependencies();
            CreateModelStates();
            StartModel();
            EnablePresenters();
        }

        private void EnablePresenters()
        {
            core.input.Enable();
            core.hud.Enable();
            core.cells.Enable();
            core.blocks.Enable();
            core.boosterInventory.Enable();
            core.pause.Enable();
            core.endGame.Enable();
        }

        private void DisablePresenters()
        {
            core.input.Disable();
            core.hud.Disable();
            core.cells.Disable();
            core.blocks.Disable();
            core.boosterInventory.Disable();
            core.pause.Disable();
            core.endGame.Disable();
        }

        private bool ValidateSelectedLevel()
        {
            if (model.LevelProgress.CurrentLevelIndex < 0)
                return false;

            if (model.LevelProgress.CurrentLevelIndex > configProvider.LastLevelIndex)
                return false;

            return true;
        }

        private void GetSceneDependencies() => core = GameObject.FindAnyObjectByType<CoreDependencies>();

        private void CreateModelStates()
        {
            core.stateMachine.AddState(core.stateFactory.Create<LoadLevelState>());
            core.stateMachine.AddState(core.stateFactory.Create<WaitState>());
            core.stateMachine.AddState(core.stateFactory.Create<InputMoveBlockState>());
            core.stateMachine.AddState(core.stateFactory.Create<InputActivateBlockState>());
            core.stateMachine.AddState(core.stateFactory.Create<InputBoosterState>());
            core.stateMachine.AddState(core.stateFactory.Create<DestroyState>());
            core.stateMachine.AddState(core.stateFactory.Create<SpawnState>());
            core.stateMachine.AddState(core.stateFactory.Create<LoseState>());
            core.stateMachine.AddState(core.stateFactory.Create<WinState>());
            core.stateMachine.AddState(core.stateFactory.Create<BonusState>());
            core.stateMachine.AddState(core.stateFactory.Create<ExitState>());
        }

        private void StartModel()
        {
            LevelSO currentLevel = configProvider.GetLevelSO(model.LevelProgress.CurrentLevelIndex);
            core.stateMachine.EnterState<LoadLevelState, LevelSO>(currentLevel);
        }
    }
}