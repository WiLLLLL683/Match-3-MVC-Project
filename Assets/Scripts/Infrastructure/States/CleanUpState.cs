﻿using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт для очистки после кор-игры.
    /// Тут происходит отключение презентеров, отписка от событий, удаление объектов.
    /// PayLoad(bool) - после очистки возвращаться в мета-игру(true) или снова загружать кор-игру(false).
    /// </summary>
    public class CleanUpState : IPayLoadedState<bool>
    {
        private readonly IStateMachine stateMachine;

        private CoreDependencies core;

        public CleanUpState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public async UniTask OnEnter(bool isReturnToMeta, CancellationToken token)
        {
            GetSceneDependencies();
            DisableInput();
            DisablePresenters();

            if (isReturnToMeta)
            {
                stateMachine.EnterState<MetaState>();
            }
            else
            {
                stateMachine.EnterState<LoadLevelState>();
            }
        }

        public async UniTask OnExit(CancellationToken token)
        {

        }

        private void GetSceneDependencies() => core = GameObject.FindAnyObjectByType<CoreDependencies>();
        private void DisableInput() => core.input.Disable();

        private void DisablePresenters()
        {
            core.hud.Disable();
            core.cells.Disable();
            core.blocks.Disable();
            core.boosters.Disable();
            core.pause.Disable();
            core.endGame.Disable();
        }
    }
}