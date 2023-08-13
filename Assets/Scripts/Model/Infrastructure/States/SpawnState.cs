using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class SpawnState : AModelState
    {
        private Game game;
        private Level level;
        private StateMachine<AModelState> stateMachine;
        private IGravitySystem gravitySystem;
        private IMatchSystem matchSystem;
        private ISpawnSystem spawnSystem;

        private const int MAX_SPAWN_ITERATIONS = 10; //максимальное количество итераций спавна/проверки до

        public SpawnState(Game _game, StateMachine<AModelState> _stateMachine, AllSystems _systems)
        {
            game = _game;
            stateMachine = _stateMachine;
            gravitySystem = _systems.GetSystem<IGravitySystem>();
            matchSystem = _systems.GetSystem<IMatchSystem>();
            spawnSystem = _systems.GetSystem<ISpawnSystem>();
        }

        public override void OnStart()
        {
            level = game.CurrentLevel;

            for (int i = 0; i < MAX_SPAWN_ITERATIONS; i++)
            {
                //гравитация
                gravitySystem.Execute(level.gameBoard);

                //проверка на совпадения
                HashSet<Cell> matches = matchSystem.FindAllMatches();

                //если нет совпадений - прекратить
                if (matches.Count == 0)
                    break;

                //если есть - удалить совпадающие блоки
                foreach (Cell match in matches)
                {
                    level.UpdateGoals(match.Block.Type);
                    match.DestroyBlock();
                }

                //спавн верхней полосы
                spawnSystem.SpawnTopLine();
            }

            stateMachine.SetState<WaitState>();
        }

        public override void OnEnd()
        {

        }
    }
}