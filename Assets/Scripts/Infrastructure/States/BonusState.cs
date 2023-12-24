using Cysharp.Threading.Tasks;
using System.Collections;
using Utils;

namespace Infrastructure
{
    public class BonusState : IState
    {
        private readonly IStateMachine stateMachine;

        public BonusState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public async UniTask OnEnter()
        {

        }

        public async UniTask OnExit()
        {

        }
    }
}