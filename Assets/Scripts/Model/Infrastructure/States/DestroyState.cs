using Model.Objects;
using Model.Services;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
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
            ICounterTarget turnTarget)
        {
            this.stateMachine = stateMachine;
            this.blockDestroyService = blockDestroyService;
            this.winLoseService = winLoseService;
            this.turnTarget = turnTarget;
        }

        public void OnEnter(HashSet<Cell> payLoad)
        {
            DestroyBlocks(payLoad);
            stateMachine.EnterState<SpawnState>();
        }

        public void OnEnter()
        {
            Debug.LogWarning("Payloaded states should not be entered without payload, returning to WaitState state");
            stateMachine.EnterState<WaitState>();
        }

        public void OnExit()
        {

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