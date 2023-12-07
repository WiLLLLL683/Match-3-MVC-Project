using System.Collections;

namespace Utils
{
    public interface IExitableState
    {
        public IEnumerator OnExit();
    }
}