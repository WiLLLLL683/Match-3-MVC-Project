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
        private readonly IBlockMatchService matchService;
        private readonly IBlockRandomTypeService randomService;
        private readonly IBlockSpawnService spawnService;
        private readonly ILevelLoader levelLoader;

        private Level level;

        private const int MATCH_CHECK_ITERATIONS = 10; //количество итераций проверки совпавших блоков в начале уровня

        public LoadLevelState(Game game,
            IStateMachine stateMachine,
            ILevelFactory levelFactory,
            IBlockRandomTypeService randomService,
            IBlockSpawnService spawnService,
            IBlockMatchService matchService,
            ILevelLoader levelLoader)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.levelFactory = levelFactory;
            this.randomService = randomService;
            this.spawnService = spawnService;
            this.matchService = matchService;
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

            stateMachine.EnterState<WaitState>();
        }

        public void OnEnter()
        {
            Debug.LogWarning("Payloaded states should not be entered without payload, loading MetaGame");
            levelLoader.LoadMetaGame();
        }

        public void OnExit()
        {
            Debug.Log("Core Game Started");
        }

        private void LoadLevel(LevelSO levelData)
        {
            level = levelFactory.Create(levelData);
            game.CurrentLevel = level;
            randomService.SetLevelConfig(levelData.blockTypeSet.GetWeights(), levelData.blockTypeSet.defaultBlockType.type);
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