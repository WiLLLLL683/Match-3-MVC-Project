using System.Collections;

namespace Utils
{
    public abstract class StateBase : IState
    {
        public abstract IEnumerator OnEnter();

        public virtual IEnumerator OnExit()
        {
            yield break;
        }
    }
}