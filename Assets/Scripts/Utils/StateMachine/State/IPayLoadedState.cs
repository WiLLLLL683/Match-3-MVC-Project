using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;

namespace Utils
{
    public interface IPayLoadedState<TPayLoad> : IExitableState
    {
        public UniTask OnEnter(TPayLoad payLoad, CancellationToken token);
    }
}