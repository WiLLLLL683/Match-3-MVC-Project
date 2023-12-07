﻿using Config;
using Model.Objects;
using Model.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    public class DestroyState : IPayLoadedState<HashSet<Cell>>
    {
        private readonly IStateMachine stateMachine;
        private readonly IBlockDestroyService blockDestroyService;
        private readonly IWinLoseService winLoseService;
        private readonly ICounterTarget turnTarget;

        public DestroyState(IStateMachine stateMachine,
            IBlockDestroyService blockDestroyService,
            IWinLoseService winLoseService,
            IConfigProvider configProvider)
        {
            this.stateMachine = stateMachine;
            this.blockDestroyService = blockDestroyService;
            this.winLoseService = winLoseService;
            this.turnTarget = configProvider.Turn.CounterTarget;
        }

        public IEnumerator OnEnter(HashSet<Cell> payLoad)
        {
            DestroyBlocks(payLoad);
            stateMachine.EnterState<SpawnState>();
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }

        private void DestroyBlocks(HashSet<Cell> matches)
        {
            //TODO засчитать ход в логгер
            winLoseService.DecreaseCountIfPossible(turnTarget);

            foreach (Cell match in matches)
            {
                winLoseService.DecreaseCountIfPossible(match.Block.Type);
                blockDestroyService.DestroyAt(match);
            }
        }
    }
}