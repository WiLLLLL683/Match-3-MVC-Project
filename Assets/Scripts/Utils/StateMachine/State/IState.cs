using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;

namespace Utils
{
    public interface IState : IExitableState
    {
        public UniTask OnEnter(CancellationToken token);
    }
}