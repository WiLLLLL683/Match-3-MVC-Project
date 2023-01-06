using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class GameStateMachine
    {
        public EventDispatcher eventDispatcher { get; private set; }
        public IState previousState { get; private set; }
        public IState currentState { get; private set; }

        public GameStateMachine(EventDispatcher _eventDispatcher)
        {
            eventDispatcher = _eventDispatcher;
        }

        public void ChangeState(IState _state)
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