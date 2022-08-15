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
            IState newState = new LoadState();
            GameStateMachine stateMachine = new GameStateMachine();

            stateMachine.ChangeState(newState);

            Assert.AreEqual(newState, stateMachine.currentState);
        }

        [Test]
        public void ChangeState_LoadStateToWaitState_PreviousStateLoadState()
        {
            IState prevState = new LoadState();
            IState newState = new WaitState();
            GameStateMachine stateMachine = new GameStateMachine();

            stateMachine.ChangeState(prevState);
            stateMachine.ChangeState(newState);

            Assert.AreEqual(newState, stateMachine.currentState);
            Assert.AreEqual(prevState, stateMachine.previousState);
        }

        [Test]
        public void PreviousState_NullToLoadStateToNull_CurrentStateLoadState()
        {
            IState newState = new LoadState();
            GameStateMachine stateMachine = new GameStateMachine();

            stateMachine.ChangeState(newState);
            stateMachine.PrevoiusState();

            Assert.AreEqual(newState, stateMachine.currentState);
        }

        [Test]
        public void PreviousState_LoadStateToWaitStateToLoadState_CurrentStateLoadState()
        {
            IState prevState = new LoadState();
            IState newState = new WaitState();
            GameStateMachine stateMachine = new GameStateMachine();

            stateMachine.ChangeState(prevState);
            stateMachine.ChangeState(newState);
            stateMachine.PrevoiusState();

            Assert.AreEqual(prevState, stateMachine.currentState);
        }

    }
}