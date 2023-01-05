using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.GameLogic.Tests
{
    public class GameStateMachineTests
    {
        [Test]
        public void ChangeState_NullToLoadState_CurrentStateLoadState()
        {
            EventDispatcher eventDispatcher = new EventDispatcher();
            GameStateMachine stateMachine = new GameStateMachine(eventDispatcher);
            AState newState = new TestState(stateMachine);

            stateMachine.ChangeState(newState);

            Assert.AreEqual(newState, stateMachine.currentState);
        }

        [Test]
        public void ChangeState_LoadStateToWaitState_PreviousStateLoadState()
        {
            EventDispatcher eventDispatcher = new EventDispatcher();
            GameStateMachine stateMachine = new GameStateMachine(eventDispatcher);
            AState prevState = new TestState(stateMachine);
            AState newState = new TestState2(stateMachine);

            stateMachine.ChangeState(prevState);
            stateMachine.ChangeState(newState);

            Assert.AreEqual(newState, stateMachine.currentState);
            Assert.AreEqual(prevState, stateMachine.previousState);
        }

        [Test]
        public void PreviousState_NullToLoadStateToNull_CurrentStateLoadState()
        {
            EventDispatcher eventDispatcher = new EventDispatcher();
            GameStateMachine stateMachine = new GameStateMachine(eventDispatcher);
            AState newState = new TestState(stateMachine);

            stateMachine.ChangeState(newState);
            stateMachine.PrevoiusState();

            Assert.AreEqual(newState, stateMachine.currentState);
        }

        [Test]
        public void PreviousState_LoadStateToWaitStateToLoadState_CurrentStateLoadState()
        {
            EventDispatcher eventDispatcher = new EventDispatcher();
            GameStateMachine stateMachine = new GameStateMachine(eventDispatcher);
            AState prevState = new TestState(stateMachine);
            AState newState = new TestState2(stateMachine);

            stateMachine.ChangeState(prevState);
            stateMachine.ChangeState(newState);
            stateMachine.PrevoiusState();

            Assert.AreEqual(prevState, stateMachine.currentState);
        }

    }
}