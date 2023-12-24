using Cysharp.Threading.Tasks;
using System.Collections;
using Utils;

namespace Infrastructure
{
    public class WinState : IState
    {
        private readonly IStateMachine stateMachine;

        public WinState(IStateMachine stateMachine)
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