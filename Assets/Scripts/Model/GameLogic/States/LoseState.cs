using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class LoseState : ACoreGameState
    {

        public LoseState(GameStateMachine _stateMachine, StateContext _contex) : base(_stateMachine, _contex)
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