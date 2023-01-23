using System.Collections;
using UnityEngine;

namespace Model.Infrastructure
{
    public interface IState
    {
        public void OnStart();
        public void OnEnd();
    }
}