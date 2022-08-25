using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic.Tests
{
    public class TestState2 : AState
    {
        public TestState2(GameStateMachine _stateMachine) : base(_stateMachine)
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