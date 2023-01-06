using Data;
using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class LoadLevelState : IState
    {
        private Game game;
        private GameStateMachine stateMachine;
        private EventDispatcher eventDispatcher;
        private MatchSystem matchSystem;

        private LevelData levelData;
        private Level level;

        private const int MATCH_CHECK_ITERATIONS = 3; //количество итераций проверки совпавших блоков

        public LoadLevelState(Game _game, LevelData _levelData)
        {
            game = _game;
            stateMachine = _game.StateMachine;
            eventDispatcher = _game.EventDispatcher;
            matchSystem = _game.MatchSystem;
            levelData = _levelData;
        }

        public void OnStart()
        {
            if (!levelData.ValidCheck())
            {
                Debug.LogError("Invalid LevelData");
                stateMachine.ChangeState(new MetaGameState());
                return;
            }

            LoadLevel();
            SpawnBlocks();
            SwapMatchedBlocks();

            stateMachine.ChangeState(new WaitState(game));
        }

        public void OnEnd()
        {

        }



        private void LoadLevel()
        {
            level = new Level(levelData);
            game.SetLevel(level);
            eventDispatcher.SubscribeOnLevel(level);
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