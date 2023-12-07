using System.Collections;
using Utils;

namespace Model.Infrastructure
{
    public class ExitState : IState
    {
        private readonly IStateMachine stateMachine;

        public ExitState(IStateMachine stateMachine)
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