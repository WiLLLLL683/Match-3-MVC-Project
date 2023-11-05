using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Стейт-машина, хранящая по одному экземпляру добавленных в нее стейтов типа TState
    /// </summary>
    public class StateMachine<TState> : IStateMachine<TState> where TState : IState
    {
        public TState PreviousState { get; private set; }
        public TState CurrentState { get; private set; }

        private readonly Dictionary<Type, TState> states;

        public StateMachine()
        {
            states = new();
        }

        public StateMachine(Dictionary<Type, TState> states)
        {
            this.states = states;
        }

        public void EnterState<T>() where T : TState
        {
            T newState = GetState<T>();
            ChangeState(newState);
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

        public void AddState(TState state)
        {
            Type type = state.GetType();
            states[type] = state;
        }

        public T GetState<T>() where T : TState
        {
            if (!states.ContainsKey(typeof(T)))
            {
                Debug.LogError("Can't get state: " + typeof(T) + " - StateMachine doesn't contain this state");
                return default;
            }

            return (T)states[typeof(T)];
        }

        private void ChangeState(TState newState)
        {
            if (newState == null)
            {
                Debug.LogError("Attempt to load null state");
                return;
            }

            PreviousState = CurrentState;
            PreviousState?.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }
}