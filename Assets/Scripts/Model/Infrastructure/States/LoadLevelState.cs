using Data;
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
        private Game game;
        private StateMachine<AModelState> stateMachine;
        private AllSystems systems;
        private IMatchSystem matchSystem;

        private LevelData levelData;
        private Level level;

        private const int MATCH_CHECK_ITERATIONS = 3; //количество итераций проверки совпавших блоков

        public LoadLevelState(Game _game, StateMachine<AModelState> _stateMachine, AllSystems _systems)
        {
            game = _game;
            stateMachine = _stateMachine;
            systems = _systems;
            matchSystem = systems.GetSystem<IMatchSystem>();
        }

        public void SetLevelData(LevelData _levelData)
        {
            levelData = _levelData;
        }

        public override void OnStart()
        {
            if (levelData == null)
            {
                Debug.LogError("Invalid LevelData");
                //stateMachine.SetState<MetaGameState>();
                return;
            }

            LoadLevel();
            SpawnBlocks();
            //SwapMatchedBlocks(); //TODO

            Debug.Log("Core Game Started");
            stateMachine.SetState<WaitState>();
        }

        public override void OnEnd()
        {

        }



        private void LoadLevel()
        {
            level = new Level(levelData);
            game.SetCurrentLevel(level);
            systems.SetLevel(level);
        }

        private void SpawnBlocks()
        {
            for (int x = 0; x < level.gameBoard.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.Cells.GetLength(1); y++)
                {
                    SpawnRandomBlock(level, level.gameBoard.Cells[x, y]);
                }
            }
        }

        private void SwapMatchedBlocks()
        {
            for (int i = 0; i < MATCH_CHECK_ITERATIONS; i++)
            {
                HashSet<Cell> matches = matchSystem.FindMatches();
                foreach (Cell match in matches)
                {
                    match.DestroyBlock();
                    SpawnRandomBlock(level, match);
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