using System.Collections;
using UnityEngine;

namespace Model.GameLogic
{
    public abstract class AState
    {
        protected GameStateMachine stateMachine;

        public AState(GameStateMachine _stateMachine)
        {
            stateMachine = _stateMachine;
        }

        public virtual void OnStart()
        {
            Debug.Log(string.Join(" ", this.GetType().ToString(), "state is started"));
            //TODO ивент
        }

        public virtual void OnEnd()
        {
            Debug.Log(string.Join(" ", this.GetType().ToString(), "state is ended"));
            //TODO ивент
        }
    }
}