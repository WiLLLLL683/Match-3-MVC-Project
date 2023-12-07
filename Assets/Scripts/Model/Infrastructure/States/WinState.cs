using System.Collections;
using Utils;

namespace Model.Infrastructure
{
    public class WinState : IState
    {
        private readonly IStateMachine stateMachine;

        public WinState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public IEnumerator OnEnter()
        {
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }
    }
}