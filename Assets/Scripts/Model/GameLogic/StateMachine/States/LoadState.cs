using Data;
using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class LoadState : AState
    {
        private Game context;
        private LevelData levelData;

        private int matchCheckIterations = 3; //количество итераций проверки совпавших блоков

        public LoadState(GameStateMachine _stateMachine, LevelData _levelData) : base(_stateMachine)
        {
            levelData = _levelData;
        }

        public override void OnStart()
        {
            base.OnStart();

            if (!levelData.ValidCheck())
            {
                Debug.LogError("Invalid LevelData");
                stateMachine.ChangeState(new MetaState(stateMachine));
                return;
            }

            //загрузка данных уровня
            Level level = new Level(levelData);

            //загрузка систем
            context = new Game(level);
            stateMachine.ChangeState(new WaitState(stateMachine,context));

            //спавн блоков
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                for (int y = 0; y < level.gameBoard.cells.GetLength(1); y++)
                {
                    SpawnRandomBlock(level, level.gameBoard.cells[x, y]);
                }
            }

            //замена совпавших блоков
            for (int i = 0; i < matchCheckIterations; i++)
            {
                List<Cell> matches = context.MatchSystem.FindMatches();
                for (int j = 0; j < matches.Count; j++)
                {
                    matches[j].DestroyBlock();
                    SpawnRandomBlock(level, matches[j]);
                }
            }

            //подключение EventDispatcher
            stateMachine.eventDispatcher.SubscribeOnLevel(context.Level);
        }


        private static void SpawnRandomBlock(Level _level, Cell _cell)
        {
            ABlockType blockType = _level.balance.GetRandomBlockType();
            new SpawnBlockAction(_level.gameBoard, blockType, _cell).Execute();
        }
    }
}