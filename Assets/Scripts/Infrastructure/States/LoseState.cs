using System.Collections;
using Utils;

namespace Infrastructure
{
    public class LoseState : IState
    {
        private readonly IStateMachine stateMachine;

        public LoseState(IStateMachine stateMachine)
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