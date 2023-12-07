using Model.Objects;
using UnityEngine;
using Utils;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// Точка входа для всего приложения
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
        private IStateMachine stateMachine;
        private IStateFactory stateFactory;

        [Inject]
        public void Construct(IStateMachine stateMachine, IStateFactory stateFactory)
        {
            this.stateMachine = stateMachine;
            this.stateFactory = stateFactory;
        }

        private void Start()
        {
            stateMachine.AddState(stateFactory.Create<LoadGameState>());
            stateMachine.AddState(stateFactory.Create<MetaState>());
            stateMachine.AddState(stateFactory.Create<LoadLevelState>());
            stateMachine.AddState(stateFactory.Create<WaitState>());
            stateMachine.AddState(stateFactory.Create<InputMoveBlockState>());
            stateMachine.AddState(stateFactory.Create<InputActivateBlockState>());
            stateMachine.AddState(stateFactory.Create<InputBoosterState>());
            stateMachine.AddState(stateFactory.Create<DestroyState>());
            stateMachine.AddState(stateFactory.Create<SpawnState>());
            stateMachine.AddState(stateFactory.Create<LoseState>());
            stateMachine.AddState(stateFactory.Create<WinState>());
            stateMachine.AddState(stateFactory.Create<BonusState>());
            stateMachine.AddState(stateFactory.Create<CleanUpState>());

            stateMachine.EnterState<LoadGameState>();
        }
    }
}