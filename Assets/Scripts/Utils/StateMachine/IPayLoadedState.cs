using System.Collections;

namespace Utils
{
    public interface IPayLoadedState<TPayLoad> : IExitableState
    {
        public IEnumerator OnEnter(TPayLoad payLoad);
    }
}