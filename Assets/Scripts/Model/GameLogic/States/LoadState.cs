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
        private StateContext context;
        private LevelData levelData;

        private int matchCheckIterations = 3; //количество итераций проверки совпавших блоков

        public LoadState(GameStateMachine _stateMachine, LevelData _levelData) : base(_stateMachine)
        {
            levelData = _levelData;
        }

        public override void OnStart()
        {
            base.OnStart();

            if (levelData.ValidCheck())
            {
                //загрузка данных уровня
                Level level = new Level(levelData);

                //загрузка систем
                context = new StateContext(level);
                stateMachine.ChangeState(new WaitState(stateMachine,context));

                //спавн блоков
                for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
                {
                    for (int y = 0; y < level.gameBoard.cells.GetLength(1); y++)
                    {
                        SpawnRandomBlock(level, new Vector2Int(x, y));
                    }
                }

                //замена совпавших блоков
                for (int i = 0; i < matchCheckIterations; i++)
                {
                    List<Cell> matches = context.MatchSystem.FindMatches();
                    for (int j = 0; j < matches.Count; j++)
                    {
                        Vector2Int pos = matches[j].block.position;
                        matches[j].DestroyBlock();
                        SpawnRandomBlock(level, pos);
                    }
                }
            }
            else
            {
                Debug.LogError("Invalid LevelData");
                stateMachine.ChangeState(new MetaState(stateMachine));
            }
        }

        private static void SpawnRandomBlock(Level level, Vector2Int pos)
        {
            //получить тип блока
            ABlockType blockType = level.balance.GetRandomBlockType();
            //заспавнить блок
            new SpawnBlockAction(level.gameBoard, blockType, pos).Execute();
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}