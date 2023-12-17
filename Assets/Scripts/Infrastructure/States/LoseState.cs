using System.Collections;
using Utils;

namespace Infrastructure
{
    public class LoseState : StateBase
    {
        private readonly IStateMachine stateMachine;

        public LoseState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override IEnumerator OnEnter()
        {
            yield break;
        }
    }
}