using Cysharp.Threading.Tasks;
using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utils;

namespace Utils.UnitTests
{
    public class TestState : IState
    {
        public string testString;

        public async UniTask OnEnter(CancellationToken token)
        {

        }

        public async UniTask OnExit(CancellationToken token)
        {

        }
    }
}