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
        private GameStateMachine gameStateMachine;
        private IStateFactory stateFactory;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine, IStateFactory stateFactory)
        {
            this.gameStateMachine = gameStateMachine;
            this.stateFactory = stateFactory;
        }

        private void Start()
        {
            gameStateMachine.AddState(stateFactory.Create<LoadGameState>());
            gameStateMachine.AddState(stateFactory.Create<MetaState>());
            gameStateMachine.AddState(stateFactory.Create<CoreState>());

            gameStateMachine.EnterState<LoadGameState>();
        }
    }
}