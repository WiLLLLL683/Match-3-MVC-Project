using Utils;

namespace Model.Infrastructure
{
    public class BonusState : AModelState
    {
        private readonly IStateMachine<AModelState> stateMachine;

        public BonusState(IStateMachine<AModelState> stateMachine)
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