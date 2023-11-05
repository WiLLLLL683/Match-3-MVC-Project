using Utils;

namespace Model.Infrastructure
{
    public class LoseState : AModelState
    {
        private readonly IStateMachine<AModelState> stateMachine;

        public LoseState(IStateMachine<AModelState> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }
    }
}