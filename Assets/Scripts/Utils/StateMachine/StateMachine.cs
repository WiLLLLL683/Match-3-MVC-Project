using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Стейт-машина, хранящая по одному экземпляру добавленных в нее стейтов типа TState
    /// </summary>
    public class StateMachine : IStateMachine
    {
        public IState PreviousState { get; private set; }
        public IState CurrentState { get; private set; }

        private readonly Dictionary<Type, IState> states;

        public StateMachine()
        {
            states = new();
        }

        public StateMachine(Dictionary<Type, IState> states)
        {
            this.states = states;
        }

        public void EnterState<T>() where T : IState
        {
            T newState = GetState<T>();
            ChangeState(newState);
            newState.OnEnter();
        }

        public void EnterState<T, TPayLoad>(TPayLoad payLoad) where T : IPayLoadedState<TPayLoad>
        {
            T newState = GetState<T>();
            ChangeState(newState);
            newState.OnEnter(payLoad);
        }

        public void EnterPreviousState()
        {
            if (PreviousState == null)
            {
                Debug.LogWarning("There's no previous state");
                return;
            }

            ChangeState(PreviousState);
        }

        public void AddState(IState state)
        {
            Type type = state.GetType();
            states[type] = state;
        }

        public T GetState<T>() where T : IState
        {
            if (!states.ContainsKey(typeof(T)))
            {
                Debug.LogError("Can't get state: " + typeof(T) + " - StateMachine doesn't contain this state");
                return default;
            }

            return (T)states[typeof(T)];
        }

        private void ChangeState(IState newState)
        {
            if (newState == null)
            {
                Debug.LogError("Attempt to load null state");
                return;
            }

            PreviousState = CurrentState;
            PreviousState?.OnExit();
            CurrentState = newState;
        }
    }
}