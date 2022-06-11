using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class ShuffleState : IState
    {
        public void OnStart()
        {
            Debug.Log(string.Join(" ", this.GetType().ToString(), "state is started"));
        }

        public void OnEnd()
        {
            Debug.Log(string.Join(" ", this.GetType().ToString(), "state is ended"));
        }
    }
}