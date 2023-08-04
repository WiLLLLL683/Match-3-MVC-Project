﻿using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class TurnState : AModelState
    {
        private Game game;
        private Level level;
        private StateMachine<AModelState> stateMachine;
        private IMoveSystem moveSystem;
        private IMatchSystem matchSystem;

        private Vector2Int startPos;
        private Directions direction;

        public TurnState(Game game, StateMachine<AModelState> stateMachine, AllSystems systems)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            moveSystem = systems.GetSystem<IMoveSystem>();
            matchSystem = systems.GetSystem<IMatchSystem>();
        }

        public void SetInput(Vector2Int startPos, Directions direction)
        {
            this.startPos = startPos;
            this.direction = direction;
        }

        public override void OnStart()
        {
            level = game.CurrentLevel;

            if (direction == Directions.Zero)
            {
                PressBlock();
            }
            else
            {
                MoveBlock();
            }
        }

        public override void OnEnd()
        {

        }

        private void MoveBlock()
        {
            //попытка хода
            IAction swapAction = moveSystem.Move(startPos, direction);
            swapAction?.Execute();

            //проверка на результативность хода
            //List<Cell> matches = matchSystem.FindMatches();
            //if (matches.Count > 0)
            //{
            //    for (int i = 0; i < matches.Count; i++)
            //    {
            //        level.UpdateGoals(matches[i].Block.Type);
            //        matches[i].DestroyBlock();
            //    }
            //    SucsessfullTurn();
            //}
            //else
            //{
            //    swapAction.Undo();
            //    stateMachine.SetPreviousState();
            //}

            //!!!TEST!!!
            stateMachine.SetState<WaitState>();
        }

        private void PressBlock()
        {
            bool turnSucsess = level.gameBoard.Cells[startPos.x, startPos.y].Block.Activate();

            if (turnSucsess)
            {
                SucsessfullTurn();
            }
            else
            {
                stateMachine.SetPreviousState();
            }
        }

        private void SucsessfullTurn()
        {
            //TODO засчитать ход в логгер
            //TODO обновить счетчики
            stateMachine.SetState<SpawnState>();
        }
    }
}