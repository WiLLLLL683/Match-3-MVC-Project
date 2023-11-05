using Utils;

namespace Model.Infrastructure
{
    public class ExitState : AModelState
    {
        private readonly IStateMachine<AModelState> stateMachine;

        public ExitState(IStateMachine<AModelState> stateMachine)
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