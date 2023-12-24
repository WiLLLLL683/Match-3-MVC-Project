using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Utils.UnitTests
{
    public class TestState : IState
    {
        public string testString;

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