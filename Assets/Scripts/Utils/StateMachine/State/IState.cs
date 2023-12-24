using Cysharp.Threading.Tasks;
using System.Collections;

namespace Utils
{
    public interface IState : IExitableState
    {
        public UniTask OnEnter();
    }
}