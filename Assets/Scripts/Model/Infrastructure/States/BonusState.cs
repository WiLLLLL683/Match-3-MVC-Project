using Utils;

namespace Model.Infrastructure
{
    public class BonusState : IState
    {
        private readonly IStateMachine stateMachine;

        public BonusState(IStateMachine stateMachine)
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