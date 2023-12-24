using Cysharp.Threading.Tasks;
using System.Collections;

namespace Utils
{
    public interface IPayLoadedState<TPayLoad> : IExitableState
    {
        public UniTask OnEnter(TPayLoad payLoad);
    }
}