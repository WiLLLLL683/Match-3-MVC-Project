using Cysharp.Threading.Tasks;
using Infrastructure;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Utils
{
    /// <summary>
    /// Стейт-машина, хранящая по одному экземпляру добавленных в нее стейтов типа TState
    /// </summary>
    public class StateMachine : IStateMachine
    {
        public IExitableState CurrentState { get; private set; }

        private readonly Dictionary<Type, IExitableState> states = new();

        public void EnterState<T>() where T : IState => EnterStateAsync<T>().Forget();

        public void EnterState<T, TPayLoad>(TPayLoad payLoad) where T : IPayLoadedState<TPayLoad> =>
            EnterStateAsync<T, TPayLoad>(payLoad).Forget();

        public async UniTask EnterStateAsync<T>() where T : IState
        {
            T newState = GetState<T>();
            if (newState == null)
                return;

            await ChangeState(newState);
            await newState.OnEnter();
        }

        public async UniTask EnterStateAsync<T, TPayLoad>(TPayLoad payLoad) where T : IPayLoadedState<TPayLoad>
        {
            T newState = GetState<T>();
            if (newState == null)
                return;

            await ChangeState(newState);
            await newState.OnEnter(payLoad);
        }

        public void AddState(IExitableState state)
        {
            Type type = state.GetType();
            states[type] = state;
        }

        public T GetState<T>() where T : IExitableState
        {
            if (!states.ContainsKey(typeof(T)))
            {
                Debug.LogError("Can't get state: " + typeof(T) + " - StateMachine doesn't contain this state");
                return default;
            }

            return (T)states[typeof(T)];
        }

        private async UniTask ChangeState(IExitableState newState)
        {
            if (CurrentState != null)
            {
                await CurrentState.OnExit();
            }

            CurrentState = newState;
        }
    }
}