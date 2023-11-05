using Utils;

namespace Model.Infrastructure
{
    public class WinState : AModelState
    {
        private readonly IStateMachine<AModelState> stateMachine;

        public WinState(IStateMachine<AModelState> stateMachine)
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