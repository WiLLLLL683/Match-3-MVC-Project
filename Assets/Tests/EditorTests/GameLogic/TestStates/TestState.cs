using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic.Tests
{
    public class TestState : AState
    {
        public TestState(GameStateMachine _stateMachine) : base(_stateMachine)
        {

        }

        public override void OnStart()
        {
            base.OnStart();
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}