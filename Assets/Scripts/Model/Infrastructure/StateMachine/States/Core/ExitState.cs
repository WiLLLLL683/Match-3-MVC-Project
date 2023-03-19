using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
{
    public class ExitState : IState
    {
        private StateMachine stateMachine;

        public ExitState(StateMachine _stateMachine, AllSystems _systems)
        {
            stateMachine = _stateMachine;
        }

        public void OnStart()
        {

        }

        public void OnEnd()
        {

        }
    }
}