using Utils;

namespace Model.Infrastructure
{
    public class LoseState : IState
    {
        private readonly IStateMachine stateMachine;

        public LoseState(IStateMachine stateMachine)
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