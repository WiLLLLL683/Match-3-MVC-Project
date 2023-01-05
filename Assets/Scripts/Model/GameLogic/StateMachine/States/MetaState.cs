using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class MetaState : AState
    {
        public MetaState(GameStateMachine _stateMachine) : base(_stateMachine)
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