using Utils;

namespace Model.Infrastructure
{
    public class LoseState : AModelState
    {
        private readonly StateMachine<AModelState> stateMachine;

        public LoseState(StateMachine<AModelState> stateMachine)
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