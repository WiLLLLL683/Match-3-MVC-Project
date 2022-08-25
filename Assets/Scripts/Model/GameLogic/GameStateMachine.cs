using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class GameStateMachine
    {
        //private List<IState> allStates;
        //public Dictionary<Type,IState> allStates { get; private set; }

        public AState previousState { get; private set; }
        public AState currentState { get; private set; }

        //public GameStateMachine(IState[] _states)
        //{
        //    allStates = new Dictionary<Type, IState>();
        //    for (int i = 0; i < _states.Length; i++)
        //    {
        //        allStates.Add(_states[i].GetType(), _states[i]);
        //    }
        //}

        //public void ChangeState(Type T where T: IState)
        //{
        //    if (currentState != null)
        //    {
        //        previousState = currentState;
        //        previousState.OnEnd();
        //    }

        //    currentState = _state;
        //    currentState.OnStart();
        //}

        public void ChangeState(AState _state)
        {
            if (currentState != null)
            {
                previousState = currentState;
                previousState.OnEnd();
            }

            currentState = _state;
            currentState.OnStart();
        }

        public void PrevoiusState()
        {
            if (previousState != null)
            {
                ChangeState(previousState);
            }
        }
    }
}