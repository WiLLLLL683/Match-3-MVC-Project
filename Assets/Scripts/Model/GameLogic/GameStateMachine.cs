using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class GameStateMachine
    {
        private List<IState> allStates;

        private IState previousState;
        private IState currentState;

        public void ChangeState(IState _state)
        {
            previousState = currentState;
            previousState.OnEnd();
            currentState = _state;
            currentState.OnStart();
        }

        public void PrevoiusState()
        {
            ChangeState(previousState);
        }
    }
}