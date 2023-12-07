using System.Collections;

namespace Utils
{
    public interface IState : IExitableState
    {
        public IEnumerator OnEnter();
    }
}