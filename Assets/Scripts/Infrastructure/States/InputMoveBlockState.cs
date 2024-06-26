﻿using Config;
using Cysharp.Threading.Tasks;
using Infrastructure.Commands;
using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        private readonly IWinLoseService winLoseService;
        private readonly ICounterTarget turnTarget;

        private Vector2Int startPos;
        private Directions direction;

        public InputMoveBlockState(IStateMachine stateMachine,
            IBlockMatchService matchService,
            IBlockMoveService moveService,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.matchService = matchService;
            this.moveService = moveService;
            this.winLoseService = winLoseService;
            this.turnTarget = configProvider.Turn.CounterTarget;
        }

        public async UniTask OnEnter((Vector2Int startPos, Directions direction) payLoad, CancellationToken token)
        {
            startPos = payLoad.startPos;
            direction = payLoad.direction;

            //бездействие при долгом зажатии блока на одном месте
            if (direction == Directions.Zero)
            {
                stateMachine.EnterState<WaitState>();
                return;
            }

            MoveBlock();
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }

        private void MoveBlock()
        {
            ICommand moveAction = new BlockMoveCommand(startPos, startPos + direction.ToVector2Int(), moveService);
            moveAction.Execute();

            HashSet<Cell> matches = matchService.FindAllMatches();

            if (matches.Count > 0)
            {
                winLoseService.DecreaseCountIfPossible(turnTarget);
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