using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Infrastructure.Tests
{
    public class GameStateMachineTests
    {
        [Test]
        public void SetState_NullToLoadState_CurrentStateLoadState()
        {
            StateMachine stateMachine = new StateMachine(
                new Dictionary<System.Type, IState>
                {
                    [typeof(TestState)] = new TestState()
                });

            stateMachine.SetState<TestState>();

            Assert.AreEqual(typeof(TestState), stateMachine.currentState.GetType());
        }

        [Test]
        public void SetState_LoadStateToWaitState_PreviousStateLoadState()
        {
            StateMachine stateMachine = new StateMachine(
                new Dictionary<System.Type, IState>
                {
                    [typeof(TestState)] = new TestState(),
                    [typeof(TestState2)] = new TestState2()
                });

            stateMachine.SetState<TestState>();
            stateMachine.SetState<TestState2>();

            Assert.AreEqual(typeof(TestState2), stateMachine.currentState.GetType());
            Assert.AreEqual(typeof(TestState), stateMachine.previousState.GetType());
        }

        [Test]
        public void SetPreviousState_NullToLoadStateToNull_CurrentStateLoadState()
        {
            StateMachine stateMachine = new StateMachine(
                new Dictionary<System.Type, IState>
                {
                    [typeof(TestState)] = new TestState()
                });

            stateMachine.SetState<TestState>();
            stateMachine.SetPrevoiusState();

            Assert.AreEqual(typeof(TestState), stateMachine.currentState.GetType());
        }

        [Test]
        public void SetPreviousState_LoadStateToWaitStateToLoadState_CurrentStateLoadState()
        {
            StateMachine stateMachine = new StateMachine(
                new Dictionary<System.Type, IState>
                {
                    [typeof(TestState)] = new TestState(),
                    [typeof(TestState2)] = new TestState2()
                });

            stateMachine.SetState<TestState>();
            stateMachine.SetState<TestState2>();
            stateMachine.SetPrevoiusState();

            Assert.AreEqual(typeof(TestState), stateMachine.currentState.GetType());
        }
        [Test]
        public void GetState_ValidState_ReferenceToValidState()
        {
            IState state = new TestState();
            StateMachine stateMachine = new StateMachine(
                new Dictionary<System.Type, IState>
                {
                    [typeof(TestState)] = state
                });

            IState receivedState = stateMachine.GetState<TestState>();

            Assert.AreEqual(state, receivedState);
        }
        [Test]
        public void GetState_InValidState_Error()
        {
            StateMachine stateMachine = new StateMachine(new Dictionary<System.Type, IState>());

            IState receivedState = stateMachine.GetState<TestState>();

            LogAssert.Expect(LogType.Error, "Can't get state: " + typeof(TestState) + " - StateMachine doesn't contain this state");
        }
        [Test]
        public void AddState_NewState_StateAdded()
        {
            IState state = new TestState();
            StateMachine stateMachine = new StateMachine();

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
            StateMachine stateMachine = new StateMachine(
                new Dictionary<System.Type, IState>
                {
                    [typeof(TestState)] = state
                });

            stateMachine.AddState(state2);

            TestState receivedState = (TestState)stateMachine.GetState<TestState>();

            Assert.AreEqual("state2", receivedState.testString);
        }

    }
}