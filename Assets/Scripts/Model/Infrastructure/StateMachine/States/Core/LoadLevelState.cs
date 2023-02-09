using Data;
using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
{
    public class LoadLevelState : IState
    {
        private Game game;
        private StateMachine stateMachine;
        //private EventDispatcher eventDispatcher;
        private AllSystems systems;
        private IMatchSystem matchSystem;

        private LevelData levelData;
        private Level level;

        private const int MATCH_CHECK_ITERATIONS = 3; //количество итераций проверки совпавших блоков

        public LoadLevelState(Game _game)
        {
            game = _game;
            stateMachine = _game.StateMachine;
            //eventDispatcher = _game.EventDispatcher;
            systems = _game.Systems;
            matchSystem = _game.Systems.MatchSystem;
        }

        public void SetLevelData(LevelData _levelData)
        {
            levelData = _levelData;
        }

        public void OnStart()
        {
            if (levelData == null)
            {
                Debug.LogError("Invalid LevelData");
                stateMachine.SetState<MetaGameState>();
                return;
            }

            LoadLevel();
            SpawnBlocks();
            SwapMatchedBlocks();

            stateMachine.SetState<WaitState>();
        }

        public void OnEnd()
        {

        }



        private void LoadLevel()
        {
            level = new Level(levelData);
            game.SetLevel(level);
            systems.SetLevel(level);
            //eventDispatcher.SubscribeOnLevel(level);
        }

        private void SpawnBlocks()
        {
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1); y++)
                {
                    SpawnRandomBlock(level, level.gameBoard.cells[x, y]);
                }
            }
        }

        private void SwapMatchedBlocks()
        {
            for (int i = 0; i < MATCH_CHECK_ITERATIONS; i++)
            {
                List<Cell> matches = matchSystem.FindMatches();
                for (int j = 0; j < matches.Count; j++)
                {
                    matches[j].DestroyBlock();
                    SpawnRandomBlock(level, matches[j]);
                }
            }
        }

        private static void SpawnRandomBlock(Level _level, Cell _cell)
        {
            ABlockType blockType = _level.balance.GetRandomBlockType();
            new SpawnBlockAction(_level.gameBoard, blockType, _cell).Execute();
        }
    }
}