using System.Collections;
using System.Collections.Generic;
using Model.Objects;
using Model.Services;
using Utils;

namespace Infrastructure
{
    public class SpawnState : IState
    {
        private readonly Game game;
        private readonly IStateMachine stateMachine;
        private readonly IBlockMatchService matchService;
        private readonly IBlockSpawnService spawnService;
        private readonly IBlockGravityService gravityService;
        private readonly IBlockDestroyService blockDestroyService;

        private GameBoard gameBoard;

        private const int MAX_SPAWN_ITERATIONS = 10; //максимальное количество итераций спавна/проверки до

        public SpawnState(Game game, IStateMachine stateMachine, IBlockSpawnService spawnService, IBlockMatchService matchService, IBlockGravityService gravityService, IBlockDestroyService blockDestroyService)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.spawnService = spawnService;
            this.matchService = matchService;
            this.gravityService = gravityService;
            this.blockDestroyService = blockDestroyService;
        }

        public IEnumerator OnEnter()
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
                        blockDestroyService.DestroyAt(match);
                    }
                }

                //спавн верхней полосы
                spawnService.FillInvisibleRows();
            }

            stateMachine.EnterState<WaitState>();
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }
    }
}