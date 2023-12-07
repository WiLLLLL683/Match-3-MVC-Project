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
        private readonly IGameStateMachine gameStateMachine;
        private readonly IConfigProvider configProvider;
        private const string CORE_SCENE_NAME = "Core";

        private CoreDependencies core;

        public CoreState(Game model, IGameStateMachine gameStateMachine, IConfigProvider configProvider)
        {
            this.model = model;
            this.gameStateMachine = gameStateMachine;
            this.configProvider = configProvider;
        }

        public IEnumerator OnEnter()
        {
            if (!ValidateSelectedLevel())
            {
                gameStateMachine.EnterState<MetaState>();
                yield break;
            }

            yield return SceneManager.LoadSceneAsync(CORE_SCENE_NAME, LoadSceneMode.Single);
            GetSceneDependencies();
            CreateModelStates();
            StartModel();
            EnablePresenters();
        }

        public IEnumerator OnExit()
        {
            DisablePresenters();
            yield break;
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
            core.coreStateMachine.AddState(core.stateFactory.Create<LoadLevelState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<WaitState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<InputMoveBlockState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<InputActivateBlockState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<InputBoosterState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<DestroyState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<SpawnState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<LoseState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<WinState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<BonusState>());
            core.coreStateMachine.AddState(core.stateFactory.Create<ExitState>());
        }

        private void StartModel()
        {
            LevelSO currentLevel = configProvider.GetLevelSO(model.LevelProgress.CurrentLevelIndex);
            core.coreStateMachine.EnterState<LoadLevelState, LevelSO>(currentLevel);
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
    }
}