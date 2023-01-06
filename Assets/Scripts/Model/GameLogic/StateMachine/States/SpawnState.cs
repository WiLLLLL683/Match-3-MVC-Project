using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class SpawnState : IState
    {
        private Game game;
        private GameStateMachine stateMachine;
        private GravitySystem gravitySystem;
        private MatchSystem matchSystem;
        private SpawnSystem spawnSystem;
        private Level level;

        private int maxIterations = 10; //максимальное количество итераций спавна/проверки до

        public SpawnState(Game _game)
        {
            game = _game;
            stateMachine = game.StateMachine;
            gravitySystem = game.GravitySystem;
            matchSystem = game.MatchSystem;
            spawnSystem = game.SpawnSystem;
            level = game.Level;
        }

        public void OnStart()
        {
            for (int i = 0; i < maxIterations; i++)
            {
                //гравитация
                gravitySystem.Execute();

                //проверка на матчи
                List<Cell> matches = matchSystem.FindMatches();

                //удалить совпадающие блоки
                for (int j = 0; j < matches.Count; j++)
                {
                    level.UpdateGoals(matches[j].block.type);
                    matches[j].DestroyBlock();
                }

                //спавн верхней полосы
                spawnSystem.SpawnTopLine();

                //TODO если уровень полон - прекратить

            }

            stateMachine.ChangeState(new WaitState(game));
        }

        public void OnEnd()
        {

        }
    }
}