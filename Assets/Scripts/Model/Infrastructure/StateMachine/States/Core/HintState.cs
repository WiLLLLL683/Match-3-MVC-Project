using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
{
    public class HintState : IState
    {
        private StateMachine stateMachine;
        
        public HintState(StateMachine _stateMachine, AllSystems _systems)
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