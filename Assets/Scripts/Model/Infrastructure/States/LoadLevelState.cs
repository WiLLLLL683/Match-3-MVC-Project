using Data;
using Model.Factories;
using Model.Objects;
using Model.Services;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class LoadLevelState : AModelState
    {
        private readonly Game game;
        private readonly StateMachine<AModelState> stateMachine;
        private readonly AllSystems systems;
        private readonly IMatchSystem matchSystem;
        private readonly IBlockSpawnService spawnService;
        private readonly LevelFactory levelFactory;

        private LevelConfig levelData;
        private Level level;

        private const int MATCH_CHECK_ITERATIONS = 10; //количество итераций проверки совпавших блоков

        public LoadLevelState(Game game, StateMachine<AModelState> stateMachine, AllSystems systems, LevelFactory levelFactory, IBlockSpawnService spawnService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.systems = systems;
            this.levelFactory = levelFactory;
            this.spawnService = spawnService;
            matchSystem = this.systems.GetSystem<IMatchSystem>();
        }

        public void SetLevelData(LevelConfig levelData) => this.levelData = levelData;

        public override void OnStart()
        {
            if (levelData == null)
            {
                Debug.LogError("Invalid LevelData");
                return;
            }

            LoadLevel();
            spawnService.FillGameBoard();
            SwapMatchedBlocks();

            Debug.Log("Core Game Started");
            stateMachine.SetState<WaitState>();
        }

        public override void OnEnd()
        {

        }



        private void LoadLevel()
        {
            level = levelFactory.Create(levelData);
            game.SetCurrentLevel(level);
            systems.SetLevel(level);
            spawnService.SetLevel(level.gameBoard, level.balance);
        }

        private void SwapMatchedBlocks()
        {
            for (int i = 0; i < MATCH_CHECK_ITERATIONS; i++)
            {
                HashSet<Cell> matches = matchSystem.FindAllMatches();
                
                if (matches.Count == 0)
                    return;

                foreach (Cell match in matches)
                {
                    spawnService.SpawnRandomBlock_WithOverride(match);
                }
            }
        }
    }
}