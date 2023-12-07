using System;
using Utils;

namespace Infrastructure
{
   public class GameStateMachine : StateMachine, IGameStateMachine
    {
        public GameStateMachine(ICoroutineRunner coroutineRunner) : base(coroutineRunner)
        {

        }
    }
}