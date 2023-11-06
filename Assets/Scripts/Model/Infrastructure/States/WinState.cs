using Utils;

namespace Model.Infrastructure
{
    public class WinState : IState
    {
        private readonly IStateMachine stateMachine;

        public WinState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }
    }
}