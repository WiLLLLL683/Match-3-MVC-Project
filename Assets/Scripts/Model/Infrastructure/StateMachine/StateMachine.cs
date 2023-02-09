using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Infrastructure
{
    /// <summary>
    /// Стейт-машина, хранящая по одному экземпляру добавленных в нее стейтов
    /// </summary>
    public class StateMachine
    {
        public IState previousState { get; private set; }
        public IState currentState { get; private set; }
        
        private Dictionary<Type, IState> states;

        public StateMachine()
        {
            states = new();
        }
        public StateMachine(Dictionary<Type, IState> _states)
        {
            states = _states;
        }

        /// <summary>
        /// Задать текущий стейт
        /// </summary>
        public void SetState<T>() where T:IState
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
        public void SetPrevoiusState()
        {
            if (previousState == null)
            {
                Debug.LogWarning("There's no previous state");
                return;
            }

            ChangeState(previousState);
        }
        /// <summary>
        /// Получить экземпляр стейта определенного типа
        /// </summary>
        public T GetState<T>() where T : IState
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
        public void AddState(IState _state)
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


        private void ChangeState(IState _state)
        {
            if (_state == null)
            {
                Debug.LogError("Attempt to load null state");
                return;
            }

            previousState = currentState;
            previousState?.OnEnd();
            currentState = _state;
            currentState.OnStart();
        }
    }
}