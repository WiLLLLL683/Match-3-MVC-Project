using System.Collections;
using UnityEngine;

namespace Model.GameLogic
{
    public interface IState
    {
        public void OnStart();
        public void OnEnd();
    }
}