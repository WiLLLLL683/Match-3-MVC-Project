using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class SpawnState : ACoreGameState
    {
        private int maxIterations = 10; //максимальное количество итераций спавна/проверки до

        public SpawnState(GameStateMachine _stateMachine, Game _contex) : base(_stateMachine,_contex) { }

        public override void OnStart()
        {
            base.OnStart();

            for (int i = 0; i < maxIterations; i++)
            {
                //гравитация
                context.GravitySystem.Execute();

                //проверка на матчи
                List<Cell> matches = context.MatchSystem.FindMatches();

                //удалить совпадающие блоки
                for (int j = 0; j < matches.Count; j++)
                {
                    context.Level.UpdateGoals(matches[j].block.type);
                    matches[j].DestroyBlock();
                }

                //спавн верхней полосы
                context.SpawnSystem.SpawnTopLine();

                //TODO если уровень полон - прекратить

            }

            stateMachine.ChangeState(new WaitState(stateMachine,context));
        }
    }
}