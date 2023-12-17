using Infrastructure.Commands;
using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт кор-игры для изменения модели в ответ на инпут(перемещение блока)
    /// PayLoad(Vector2Int, Directions) - позиция блока направление перемещения
    /// </summary>
    public class InputMoveBlockState : IPayLoadedState<(Vector2Int startPos, Directions direction)>
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockMatchService matchService;
        private readonly IBlockMoveService moveService;

        private Vector2Int startPos;
        private Directions direction;

        public InputMoveBlockState(IStateMachine stateMachine,
            IBlockMatchService matchService,
            IBlockMoveService moveService)
        {
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.moveService = moveService;
        }

        public IEnumerator OnEnter((Vector2Int startPos, Directions direction) payLoad)
        {
            startPos = payLoad.startPos;
            direction = payLoad.direction;

            //бездействие при долгом зажатии блока на одном месте
            if (direction == Directions.Zero)
                yield break;

            MoveBlock();
        }

        public IEnumerator OnExit()
        {
            yield break;
        }

        private void MoveBlock()
        {
            ICommand moveAction = new BlockMoveCommand(startPos, startPos + direction.ToVector2Int(), moveService);
            moveAction.Execute();

            HashSet<Cell> matches = matchService.FindAllMatches();

            if (matches.Count > 0)
            {
                stateMachine.EnterState<DestroyState, HashSet<Cell>>(matches);
            }
            else
            {
                moveAction?.Undo();
                stateMachine.EnterState<WaitState>(); //Возврат к ожиданию инпута
            }
        }
    }
}