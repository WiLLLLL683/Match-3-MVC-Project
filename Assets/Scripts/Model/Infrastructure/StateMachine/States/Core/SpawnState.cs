using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
{
    public class SpawnState : IState
    {
        private Game game;
        private Level level;
        private StateMachine stateMachine;
        private IGravitySystem gravitySystem;
        private IMatchSystem matchSystem;
        private ISpawnSystem spawnSystem;

        private int maxIterations = 10; //максимальное количество итераций спавна/проверки до

        public SpawnState(Game _game)
        {
            game = _game;
            stateMachine = _game.StateMachine;
            gravitySystem = _game.Systems.GravitySystem;
            matchSystem = _game.Systems.MatchSystem;
            spawnSystem = _game.Systems.SpawnSystem;
        }

        public void OnStart()
        {
            level = game.Level;

            for (int i = 0; i < maxIterations; i++)
            {
                //гравитация
                gravitySystem.Execute();

                //проверка на матчи
                List<Cell> matches = matchSystem.FindMatches();

                //удалить совпадающие блоки
                for (int j = 0; j < matches.Count; j++)
                {
                    level.UpdateGoals(matches[j].Block.Type);
                    matches[j].DestroyBlock();
                }

                //спавн верхней полосы
                spawnSystem.SpawnTopLine();

                //TODO если уровень полон - прекратить

            }

            stateMachine.SetState<WaitState>();
        }

        public void OnEnd()
        {

        }
    }
}