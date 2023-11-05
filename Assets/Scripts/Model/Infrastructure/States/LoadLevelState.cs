using Config;
using Infrastructure;
using Model.Factories;
using Model.Objects;
using Model.Services;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class LoadLevelState : IPayLoadedState<LevelSO>
    {
        private readonly Game game;
        private readonly IStateMachine stateMachine;
        private readonly ILevelFactory levelFactory;
        private readonly IMatchService matchService;
        private readonly IRandomBlockTypeService randomService;
        private readonly IBlockSpawnService spawnService;
        private readonly IValidationService validationService;
        private readonly IGravityService gravityService;
        private readonly IBlockMoveService moveService;
        private readonly IBlockDestroyService destroyService;
        private readonly IWinLoseService winLoseService;
        private readonly LevelLoader levelLoader;

        private Level level;

        private const int MATCH_CHECK_ITERATIONS = 10; //количество итераций проверки совпавших блоков

        public LoadLevelState(Game game,
            IStateMachine stateMachine,
            ILevelFactory levelFactory,
            IValidationService validationService,
            IRandomBlockTypeService randomService,
            IBlockSpawnService spawnService,
            IMatchService matchService,
            IGravityService gravityService,
            IBlockMoveService moveService,
            IBlockDestroyService destroyService,
            IWinLoseService winLoseService,
            LevelLoader levelLoader)
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
            this.winLoseService = winLoseService;
            this.levelLoader = levelLoader;
        }

        public void OnEnter(LevelSO payLoad)
        {
            if (payLoad == null)
            {
                Debug.LogError("Invalid LevelData");
                return;
            }

            LoadLevel(payLoad);
            spawnService.FillGameBoard();
            SwapMatchedBlocks();

            Debug.Log("Core Game Started");
            stateMachine.EnterState<WaitState>();
        }

        public void OnEnter()
        {
            Debug.LogWarning("Payloaded states should not be entered without payload, loading MetaGame");
            levelLoader.LoadMetaGame();
        }

        public void OnExit()
        {

        }

        private void LoadLevel(LevelSO levelData)
        {
            level = levelFactory.Create(levelData);

            game.CurrentLevel = level;
            validationService.SetLevel(level.gameBoard);
            randomService.SetLevel(levelData.blockTypeSet.GetWeights(), levelData.blockTypeSet.defaultBlockType.type);
            spawnService.SetLevel(level.gameBoard);
            matchService.SetLevel(level.gameBoard, level.matchPatterns);
            gravityService.SetLevel(level.gameBoard);
            moveService.SetLevel(level.gameBoard);
            destroyService.SetLevel(level.gameBoard);
            winLoseService.SetLevel(level);
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