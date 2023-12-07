using System.Collections;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    public class CleanUpState : IPayLoadedState<bool>
    {
        private readonly IStateMachine stateMachine;

        private CoreDependencies core;

        public CleanUpState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public IEnumerator OnEnter(bool isReturnToMeta)
        {
            GetSceneDependencies();
            DisablePresenters();

            if (isReturnToMeta)
            {
                stateMachine.EnterState<MetaState>();
            }
            else
            {
                stateMachine.EnterState<LoadLevelState>();
            }

            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }

        private void GetSceneDependencies() => core = GameObject.FindAnyObjectByType<CoreDependencies>();

        private void DisablePresenters()
        {
            core.input.Disable();
            core.hud.Disable();
            core.cells.Disable();
            core.blocks.Disable();
            core.boosterInventory.Disable();
            core.pause.Disable();
            core.endGame.Disable();
        }
    }
}