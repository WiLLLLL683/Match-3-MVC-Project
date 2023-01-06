using Data;
using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class LoadCoreGameState : IState
    {
        private Game game;
        private GameStateMachine stateMachine;
        private LevelData levelData;
        private Level level;

        private int MATCH_CHECK_ITERATIONS = 3; //количество итераций проверки совпавших блоков

        public LoadCoreGameState(Game _game, LevelData _levelData)
        {
            game = _game;
            stateMachine = game.StateMachine;
            levelData = _levelData;
        }

        public void OnStart()
        {
            if (!levelData.ValidCheck())
            {
                Debug.LogError("Invalid LevelData");
                stateMachine.ChangeState(new MetaState());
                return;
            }

            level = new Level(levelData);

            game.SetLevel(level);
            stateMachine.eventDispatcher.SubscribeOnLevel(game.Level);

            SpawnBlocks();
            SwapMatchedBlocks();

            stateMachine.ChangeState(new WaitState(game));
        }

        private void SwapMatchedBlocks()
        {
            //замена совпавших блоков
            for (int i = 0; i < MATCH_CHECK_ITERATIONS; i++)
            {
                List<Cell> matches = game.MatchSystem.FindMatches();
                for (int j = 0; j < matches.Count; j++)
                {
                    matches[j].DestroyBlock();
                    SpawnRandomBlock(level, matches[j]);
                }
            }
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

        public void OnEnd()
        {

        }

        private static void SpawnRandomBlock(Level _level, Cell _cell)
        {
            ABlockType blockType = _level.balance.GetRandomBlockType();
            new SpawnBlockAction(_level.gameBoard, blockType, _cell).Execute();
        }
    }
}