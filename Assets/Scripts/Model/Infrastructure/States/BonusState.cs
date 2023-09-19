using Utils;

namespace Model.Infrastructure
{
    public class BonusState : AModelState
    {
        private readonly StateMachine<AModelState> stateMachine;

        public BonusState(StateMachine<AModelState> stateMachine)
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