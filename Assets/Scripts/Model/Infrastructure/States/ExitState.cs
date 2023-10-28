using Utils;

namespace Model.Infrastructure
{
    public class ExitState : AModelState
    {
        private readonly StateMachine<AModelState> stateMachine;

        public ExitState(StateMachine<AModelState> stateMachine)
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