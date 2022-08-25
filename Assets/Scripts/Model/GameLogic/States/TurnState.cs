using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class TurnState : AState
    {
        public TurnState(GameStateMachine _stateMachine) : base(_stateMachine)
        {

        }

        public override void OnStart()
        {
            base.OnStart();
            //TODO возможно стоит вынести проверку на результативность хода в игровую логику            
            //проверка на результативность хода
            //if (matchSystem.FindMatches().Count > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    swapAction.Undo();
            //    return false;
            //}
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}