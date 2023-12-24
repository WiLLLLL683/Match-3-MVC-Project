using Cysharp.Threading.Tasks;
using System.Collections;

namespace Utils
{
    public interface IExitableState
    {
        public UniTask OnExit();
    }
}