using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class TurnState : IState
    {
        public void OnStart()
        {
            Debug.Log(string.Join(" ", this.GetType().ToString(), "state is started"));

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

        public void OnEnd()
        {
            Debug.Log(string.Join(" ", this.GetType().ToString(), "state is ended"));
        }
    }
}