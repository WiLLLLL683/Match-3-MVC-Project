﻿using Cysharp.Threading.Tasks;
using System.Collections;
using Utils;

namespace Infrastructure
{
    public class LoseState : IState
    {
        private readonly IStateMachine stateMachine;

        public LoseState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public async UniTask OnEnter()
        {

        }

        public async UniTask OnExit()
        {

        }
    }
}