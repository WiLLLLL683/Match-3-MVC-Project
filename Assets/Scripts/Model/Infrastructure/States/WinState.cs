using Utils;

namespace Model.Infrastructure
{
    public class WinState : AModelState
    {
        private readonly StateMachine<AModelState> stateMachine;

        public WinState(StateMachine<AModelState> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void OnStart()
        {

        }

        public override void OnEnd()
        {

        }
    }
}