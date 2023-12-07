using System;
using Utils;

namespace Infrastructure
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(ICoroutineRunner coroutineRunner) : base(coroutineRunner)
        {

        }
    }
}