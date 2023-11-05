using Utils;

namespace Model.Infrastructure
{
    public class ExitState : IState
    {
        private readonly IStateMachine stateMachine;

        public ExitState(IStateMachine stateMachine)
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