using System.Collections;
using Utils;

namespace Infrastructure
{
    public class BonusState : StateBase
    {
        private readonly IStateMachine stateMachine;

        public BonusState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override IEnumerator OnEnter()
        {
            yield break;
        }
    }
}