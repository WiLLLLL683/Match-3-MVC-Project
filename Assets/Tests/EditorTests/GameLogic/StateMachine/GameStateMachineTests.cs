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
            StateMachine stateMachine = new StateMachine();
            IState newState = new TestState();

            stateMachine.ChangeState(newState);

            Assert.AreEqual(newState, stateMachine.currentState);
        }

        [Test]
        public void ChangeState_LoadStateToWaitState_PreviousStateLoadState()
        {
            StateMachine stateMachine = new StateMachine();
            IState prevState = new TestState();
            IState newState = new TestState2();

            stateMachine.ChangeState(prevState);
            stateMachine.ChangeState(newState);

            Assert.AreEqual(newState, stateMachine.currentState);
            Assert.AreEqual(prevState, stateMachine.previousState);
        }

        [Test]
        public void PreviousState_NullToLoadStateToNull_CurrentStateLoadState()
        {
            StateMachine stateMachine = new StateMachine();
            IState newState = new TestState();

            stateMachine.ChangeState(newState);
            stateMachine.PrevoiusState();

            Assert.AreEqual(newState, stateMachine.currentState);
        }

        [Test]
        public void PreviousState_LoadStateToWaitStateToLoadState_CurrentStateLoadState()
        {
            StateMachine stateMachine = new StateMachine();
            IState prevState = new TestState();
            IState newState = new TestState2();

            stateMachine.ChangeState(prevState);
            stateMachine.ChangeState(newState);
            stateMachine.PrevoiusState();

            Assert.AreEqual(prevState, stateMachine.currentState);
        }

    }
}