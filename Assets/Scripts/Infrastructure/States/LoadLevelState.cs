﻿using Config;
using Cysharp.Threading.Tasks;
using Model.Factories;
using Model.Objects;
using Model.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using View.Input;

namespace Infrastructure
{
    /// <summary>
    /// Стейт для загрузки уровня кор-игры, заполнения игрового поля и подключения презентеров
    /// После загрузки переход в WaitState, при неудаче в CleanUpState -> MetaState
    /// </summary>
    public class LoadLevelState : IState
    {
        private readonly Game model;
        private readonly SceneLoader sceneLoader;
        private readonly IStateMachine stateMachine;
        private readonly IConfigProvider configProvider;
        private readonly ILevelFactory levelFactory;
        private readonly IBlockTypeFactory randomService;
        private readonly IBlockSpawnService spawnService;
        private readonly IBlockMatchService matchService;

        private const string CORE_SCENE_NAME = "Core";
        private const string META_SCENE_NAME = "Meta";
        private const int MATCH_CHECK_ITERATIONS = 10; //количество итераций проверки совпавших блоков в начале уровня

        private CoreDependencies core;

        public LoadLevelState(Game model,
            SceneLoader sceneLoader,
            IStateMachine stateMachine,
            IConfigProvider configProvider,
            ILevelFactory levelFactory,
            IBlockTypeFactory randomService,
            IBlockSpawnService spawnService,
            IBlockMatchService matchService)
        {
            this.model = model;
            this.sceneLoader = sceneLoader;
            this.stateMachine = stateMachine;
            this.configProvider = configProvider;
            this.levelFactory = levelFactory;
            this.randomService = randomService;
            this.spawnService = spawnService;
            this.matchService = matchService;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            if (!ValidateSelectedLevel())
            {
                stateMachine.EnterState<CleanUpState, bool>(true);
                return;
            }

            await sceneLoader.LoadScene(CORE_SCENE_NAME, LoadSceneMode.Additive, CORE_SCENE_NAME, META_SCENE_NAME);
            GetSceneDependencies();

            if (!LoadLevel())
            {
                stateMachine.EnterState<CleanUpState, bool>(true);
                return;
            }

            FillGameBoardModel();
            SwapMatchedBlocks();
            EnableInput();
            EnablePresenters();
            stateMachine.EnterState<WaitState>();
        }

        public async UniTask OnExit(CancellationToken token)
        {

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

        private bool LoadLevel()
        {
            LevelSO currentLevel = configProvider.GetLevelSO(model.LevelProgress.CurrentLevelIndex);
            if (currentLevel == null)
            {
                Debug.LogError("Invalid LevelData");
                return false;
            }

            model.CurrentLevel = levelFactory.Create(currentLevel);
            randomService.SetLevelConfig(currentLevel.blockTypeSet.GetWeights(), currentLevel.blockTypeSet.defaultBlockType.type);
            return true;
        }

        private void FillGameBoardModel() => spawnService.FillGameBoard();

        private void SwapMatchedBlocks()
        {
            for (int i = 0; i < MATCH_CHECK_ITERATIONS; i++)
            {
                HashSet<Cell> matches = matchService.FindAllMatches();

                if (matches.Count == 0)
                    return;

                foreach (Cell match in matches)
                {
                    spawnService.SpawnRandomBlock_WithOverride(match);
                }
            }
        }

        private void EnableInput()
        {
            core.input.Enable();
            core.input.SetCurrentMode<IMoveInputMode>();
        }

        private void EnablePresenters()
        {
            core.hud.Enable();
            core.cells.Enable();
            core.blocks.Enable();
            core.boosters.Enable();
            core.pause.Enable();
            core.endGame.Enable();
        }
    }
}