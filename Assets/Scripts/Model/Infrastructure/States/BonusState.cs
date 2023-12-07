using System.Collections;
using Utils;

namespace Model.Infrastructure
{
    public class BonusState : IState
    {
        private readonly IStateMachine stateMachine;

        public BonusState(IStateMachine stateMachine)
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