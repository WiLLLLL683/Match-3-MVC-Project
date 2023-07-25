using System.Collections;
using UnityEngine;

namespace Utils
{
    public interface IState
    {
        public void OnStart();
        public void OnEnd();
    }
}