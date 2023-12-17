using System.Collections;
using Utils;

namespace Infrastructure
{
    public class WinState : StateBase
    {
        private readonly IStateMachine stateMachine;

        public WinState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override IEnumerator OnEnter()
        {
            yield break;
        }
    }
}