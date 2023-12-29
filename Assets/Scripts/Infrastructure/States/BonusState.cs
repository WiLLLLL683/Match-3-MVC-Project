using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
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

        public async UniTask OnEnter(CancellationToken token)
        {

        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}