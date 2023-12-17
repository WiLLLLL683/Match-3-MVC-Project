using System.Collections;

namespace Utils
{
    public abstract class PayLoadedStateBase<TPayLoad> : IPayLoadedState<TPayLoad>
    {
        public abstract IEnumerator OnEnter(TPayLoad payLoad);

        public virtual IEnumerator OnExit()
        {
            yield break;
        }
    }
}