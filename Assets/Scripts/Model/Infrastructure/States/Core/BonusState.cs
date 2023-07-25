using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class BonusState : IState
    {
        private StateMachine stateMachine;

        public BonusState(StateMachine _stateMachine, AllSystems _systems)
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