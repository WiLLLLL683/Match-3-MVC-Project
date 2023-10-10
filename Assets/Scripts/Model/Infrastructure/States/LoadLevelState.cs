using Config;
using Model.Factories;
using Model.Objects;
using Model.Services;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class LoadLevelState : AModelState
    {
        private readonly Game game;
        private readonly StateMachine<AModelState> stateMachine;
        private readonly ILevelFactory levelFactory;
        private readonly IMatchService matchService;
        private readonly IRandomBlockTypeService randomBlockTypeService;
        private readonly IBlockSpawnService spawnService;
        private readonly IValidationService validationService;

        private LevelSO levelData;
        private Level level;

        private const int MATCH_CHECK_ITERATIONS = 10; //количество итераций проверки совпавших блоков

        public LoadLevelState(Game game, StateMachine<AModelState> stateMachine, ILevelFactory levelFactory, IRandomBlockTypeService randomBlockTypeService, IBlockSpawnService spawnService, IValidationService validationService, IMatchService matchService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.levelFactory = levelFactory;
            this.randomBlockTypeService = randomBlockTypeService;
            this.spawnService = spawnService;
            this.validationService = validationService;
            this.matchService = matchService;
        }

        public void SetLevelData(LevelSO levelData) => this.levelData = levelData;

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

            game.CurrentLevel = level;
            randomBlockTypeService.SetLevel(levelData.blockTypeSet.GetWeights(), levelData.blockTypeSet.defaultBlockType.type);
            spawnService.SetLevel(level.gameBoard);
            validationService.SetLevel(level.gameBoard);
            matchService.SetLevel(level.gameBoard, level.matchPatterns, level.hintPatterns);
        }

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
    }
}