﻿using System.Collections.Generic;
using Model.Objects;
using Model.Services;
using Utils;

namespace Model.Infrastructure
{
    public class SpawnState : AModelState
    {
        private readonly Game game;
        private readonly StateMachine<AModelState> stateMachine;
        private readonly IMatchService matchService;
        private readonly IBlockSpawnService spawnService;
        private readonly IGravityService gravityService;
        private readonly IBlockDestroyService blockDestroyService;

        private GameBoard gameBoard;

        private const int MAX_SPAWN_ITERATIONS = 10; //максимальное количество итераций спавна/проверки до

        public SpawnState(Game game, StateMachine<AModelState> stateMachine, IBlockSpawnService spawnService, IMatchService matchService, IGravityService gravityService, IBlockDestroyService blockDestroyService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.spawnService = spawnService;
            this.matchService = matchService;
            this.gravityService = gravityService;
            this.blockDestroyService = blockDestroyService;
        }

        public override void OnStart()
        {
            gameBoard = game.CurrentLevel.gameBoard;

            for (int i = 0; i < MAX_SPAWN_ITERATIONS; i++)
            {
                //гравитация
                gravityService.Execute();

                //проверка на совпадения
                HashSet<Cell> matches = matchService.FindAllMatches();

                //если есть совпадениz - удалить совпадающие блоки
                if (matches.Count > 0)
                {
                    foreach (Cell match in matches)
                    {
                        //level.UpdateGoals(match.Block.Type);
                        blockDestroyService.Destroy(match);
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