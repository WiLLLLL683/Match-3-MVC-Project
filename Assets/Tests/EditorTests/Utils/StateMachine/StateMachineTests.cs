using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.TestTools;
using Utils;
using Infrastructure;

namespace Utils.UnitTests
{
    public class StateMachineTests
    {
        private StateMachine Setup(params IExitableState[] states)
        {
            var coroutineRunner = new GameObject().AddComponent<CoroutineRunner>();
            var stateMachine = new StateMachine(coroutineRunner);

            for (int i = 0; i < states.Length; i++)
            {
                stateMachine.AddState(states[i]);
            }

            return stateMachine;
        }

        [Test]
        public void SetState_NullToLoadState_CurrentStateLoadState()
        {
            StateMachine stateMachine = Setup(new TestState());

            stateMachine.EnterState<TestState>();

            Assert.AreEqual(typeof(TestState), stateMachine.CurrentState.GetType());
        }

        [Test]
        public void SetState_LoadStateToWaitState_PreviousStateLoadState()
        {
            StateMachine stateMachine = Setup(new TestState(), new TestState2());

            stateMachine.EnterState<TestState>();
            stateMachine.EnterState<TestState2>();

            Assert.AreEqual(typeof(TestState2), stateMachine.CurrentState.GetType());
        }

        [Test]
        public void GetState_ValidState_ReferenceToValidState()
        {
            IState state = new TestState();
            StateMachine stateMachine = Setup(state);

            IState receivedState = stateMachine.GetState<TestState>();

            Assert.AreEqual(state, receivedState);
        }

        [Test]
        public void GetState_InValidState_Error()
        {
            StateMachine stateMachine = Setup();

            IState receivedState = stateMachine.GetState<TestState>();

            LogAssert.Expect(LogType.Error, "Can't get state: " + typeof(TestState) + " - StateMachine doesn't contain this state");
        }

        [Test]
        public void AddState_NewState_StateAdded()
        {
            IState state = new TestState();
            StateMachine stateMachine = Setup();

            stateMachine.AddState(state);

            IState receivedState = stateMachine.GetState<TestState>();

            Assert.AreEqual(state, receivedState);
        }

        [Test]
        public void AddState_ExistingState_StateReplaced()
        {
            TestState state = new TestState();
            state.testString = "state";
            TestState state2 = new TestState();
            state2.testString = "state2";
            StateMachine stateMachine = Setup(state);

            stateMachine.AddState(state2);

            TestState receivedState = (TestState)stateMachine.GetState<TestState>();

            Assert.AreEqual("state2", receivedState.testString);
        }
    }
}