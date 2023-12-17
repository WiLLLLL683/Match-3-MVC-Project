using System.Collections;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт для очистки после кор-игры.
    /// Тут происходит отключение презентеров, отписка от событий, удаление объектов.
    /// PayLoad(bool) - после очистки возвращаться в мета-игру(true) или снова загружать кор-игру(false).
    /// </summary>
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