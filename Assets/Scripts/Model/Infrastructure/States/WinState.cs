using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class WinState : AModelState
    {
        private StateMachine<AModelState> stateMachine;

        public WinState(StateMachine<AModelState> _stateMachine, AllSystems _systems)
        {
            stateMachine = _stateMachine;
        }
        
        public override void OnStart()
        {

        }

        public override void OnEnd()
        {

        }
    }
}