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
        private readonly IRandomBlockTypeService randomService;
        private readonly IBlockSpawnService spawnService;
        private readonly IValidationService validationService;
        private readonly IGravityService gravityService;
        private readonly IBlockMoveService moveService;
        private readonly IBlockDestroyService destroyService;

        private LevelSO levelData;
        private Level level;

        private const int MATCH_CHECK_ITERATIONS = 10; //количество итераций проверки совпавших блоков

        public LoadLevelState(Game game, 
            StateMachine<AModelState> stateMachine,
            ILevelFactory levelFactory,
            IValidationService validationService,
            IRandomBlockTypeService randomService,
            IBlockSpawnService spawnService,
            IMatchService matchService,
            IGravityService gravityService,
            IBlockMoveService moveService,
            IBlockDestroyService destroyService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.levelFactory = levelFactory;
            this.validationService = validationService;
            this.randomService = randomService;
            this.spawnService = spawnService;
            this.matchService = matchService;
            this.gravityService = gravityService;
            this.moveService = moveService;
            this.destroyService = destroyService;
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
            validationService.SetLevel(level.gameBoard);
            randomService.SetLevel(levelData.blockTypeSet.GetWeights(), levelData.blockTypeSet.defaultBlockType.type);
            spawnService.SetLevel(level.gameBoard);
            matchService.SetLevel(level.gameBoard, level.matchPatterns, level.hintPatterns);
            gravityService.SetLevel(level.gameBoard);
            moveService.SetLevel(level.gameBoard);
            destroyService.SetLevel(level.gameBoard);
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