using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;

namespace Utils
{
    public interface IExitableState
    {
        public UniTask OnExit(CancellationToken token);
    }
}