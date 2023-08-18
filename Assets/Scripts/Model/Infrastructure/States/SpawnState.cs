using Model.Objects;
using Model.Services;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class SpawnState : AModelState
    {
        private readonly Game game;
        private readonly StateMachine<AModelState> stateMachine;
        private readonly IGravitySystem gravitySystem;
        private readonly IMatchSystem matchSystem;
        private readonly IBlockSpawnService spawnService;

        private Level level;

        private const int MAX_SPAWN_ITERATIONS = 10; //максимальное количество итераций спавна/проверки до

        public SpawnState(Game game, StateMachine<AModelState> stateMachine, AllSystems systems, IBlockSpawnService spawnService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.spawnService = spawnService;
            gravitySystem = systems.GetSystem<IGravitySystem>();
            matchSystem = systems.GetSystem<IMatchSystem>();
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

                //если есть совпадениz - удалить совпадающие блоки
                if (matches.Count > 0)
                {
                    foreach (Cell match in matches)
                    {
                        //level.UpdateGoals(match.Block.Type);
                        match.DestroyBlock();
                    }
                }

                //спавн верхней полосы
                spawnService.FillInvisibleRows();
            }

            stateMachine.SetState<WaitState>();
        }

        public override void OnEnd()
        {

        }
    }
}