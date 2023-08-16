using Data;
using Model.Factories;
using Model.Objects;
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
        private readonly ISpawnSystem spwanSystem;
        private readonly LevelFactory levelFactory;

        private LevelConfig levelData;
        private Level level;

        private const int MATCH_CHECK_ITERATIONS = 10; //количество итераций проверки совпавших блоков

        public LoadLevelState(Game game, StateMachine<AModelState> stateMachine, AllSystems systems, LevelFactory levelFactory)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.systems = systems;
            this.levelFactory = levelFactory;
            matchSystem = this.systems.GetSystem<IMatchSystem>();
            spwanSystem = this.systems.GetSystem<ISpawnSystem>();
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
            spwanSystem.SpawnGameBoard();
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
                    match.DestroyBlock();
                    spwanSystem.SpawnRandomBlock(match);
                }
            }
        }
    }
}