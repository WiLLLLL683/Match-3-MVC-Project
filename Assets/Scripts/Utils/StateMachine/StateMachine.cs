using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Utils
{
    /// <summary>
    /// Стейт-машина, хранящая по одному экземпляру добавленных в нее стейтов типа TState
    /// </summary>
    public class StateMachine<TState> where TState : IState
    {
        public TState PreviousState { get; private set; }
        public TState CurrentState { get; private set; }
        
        private Dictionary<Type, TState> states;

        public StateMachine()
        {
            states = new();
        }
        public StateMachine(Dictionary<Type, TState> _states)
        {
            states = _states;
        }

        /// <summary>
        /// Задать текущий стейт
        /// </summary>
        public void SetState<T>() where T: TState
        {
            if (!states.ContainsKey(typeof(T)))
            {
                Debug.LogError("Can't change to state: " + typeof(T) + " - StateMachine doesn't contain this state");
                return;
            }

            ChangeState(states[typeof(T)]);
        }
        /// <summary>
        /// Вернуться к предыдущему стейту
        /// </summary>
        public void SetPreviousState()
        {
            if (PreviousState == null)
            {
                Debug.LogWarning("There's no previous state");
                return;
            }

            ChangeState(PreviousState);
        }
        /// <summary>
        /// Получить экземпляр стейта определенного типа
        /// </summary>
        public T GetState<T>() where T : TState
        {
            if (!states.ContainsKey(typeof(T)))
            {
                Debug.LogError("Can't get state: " + typeof(T) + " - StateMachine doesn't contain this state");
                return default;
            }

            return (T)states[typeof(T)];
        }
        /// <summary>
        /// Добавить новый стейт в стейт-машину
        /// </summary>
        public void AddState(TState _state)
        {
            Type type = _state.GetType();

            if (states.ContainsKey(type))
            {
                states[type] = _state;
            }
            else
            {
                states.Add(type, _state);
            }
        }


        private void ChangeState(TState _state)
        {
            if (_state == null)
            {
                Debug.LogError("Attempt to load null state");
                return;
            }

            PreviousState = CurrentState;
            PreviousState?.OnEnd();
            CurrentState = _state;
            CurrentState.OnStart();
        }
    }
}