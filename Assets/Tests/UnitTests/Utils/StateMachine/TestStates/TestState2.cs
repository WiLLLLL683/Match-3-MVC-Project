using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Utils.UnitTests
{
    public class TestState2 : IState
    {

        public IEnumerator OnEnter()
        {
            yield break;
        }

        public IEnumerator OnExit()
        {
            yield break;
        }
    }
}