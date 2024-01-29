using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

namespace View.Input.UnitTests
{
    public class GameBoardInputTests : InputTestFixture
    {
        private int selectMode_tapCount;
        private int selectMode_dragStartedCount;
        private int selectMode_dragCount;
        private int selectMode_dragEndedCount;
        private int moveMode_tapCount;
        private int moveMode_dragStartedCount;
        private int moveMode_dragCount;
        private int moveMode_dragEndedCount;

        private (GameBoardInput input, Match3ActionMap actionMap) SetupTest()
        {
            selectMode_tapCount = 0;
            selectMode_dragStartedCount = 0;
            selectMode_dragCount = 0;
            selectMode_dragEndedCount = 0;
            moveMode_tapCount = 0;
            moveMode_dragStartedCount = 0;
            moveMode_dragCount = 0;
            moveMode_dragEndedCount = 0;

            var actionMap = new Match3ActionMap();
            actionMap.Enable();

            //input modes
            var selectInputMode = Substitute.For<ISelectInputMode>();
            selectInputMode.WhenForAnyArgs(x => x.Tap(default)).Do(_ => selectMode_tapCount++);
            selectInputMode.WhenForAnyArgs(x => x.DragStarted(default)).Do(_ => selectMode_dragStartedCount++);
            selectInputMode.WhenForAnyArgs(x => x.Drag(default)).Do(_ => selectMode_dragCount++);
            selectInputMode.WhenForAnyArgs(x => x.DragEnded(default)).Do(_ => selectMode_dragEndedCount++);
            var moveInputMode = Substitute.For<IMoveInputMode>();
            moveInputMode.WhenForAnyArgs(x => x.Tap(default)).Do(_ => moveMode_tapCount++);
            moveInputMode.WhenForAnyArgs(x => x.DragStarted(default)).Do(_ => moveMode_dragStartedCount++);
            moveInputMode.WhenForAnyArgs(x => x.Drag(default)).Do(_ => moveMode_dragCount++);
            moveInputMode.WhenForAnyArgs(x => x.DragEnded(default)).Do(_ => moveMode_dragEndedCount++);

            //input
            var input = new GameBoardInput(actionMap, new()
            {
                [typeof(IMoveInputMode)] = moveInputMode,
                [typeof(ISelectInputMode)] = selectInputMode
            });

            input.SetCurrentMode<IMoveInputMode>();
            return (input, actionMap);
        }

        [Test]
        public void ActionMapTest()
        {
            var actionMap = new Match3ActionMap();
            var action = actionMap.GameBoard.Tap;
            action.Enable();
            var mouse = InputSystem.AddDevice<Mouse>();

            Press(mouse.press);

            Assert.That(action.WasPerformedThisFrame());
        }

        [Test]
        public void Enable_Tap_CurrentModeTap()
        {
            var (input, actionMap) = SetupTest();

            input.Enable();
            BeginTouch(0, new());
            EndTouch(0, new());

            Assert.AreEqual(1, moveMode_tapCount);
        }

        [Test]
        public void Enable_DragStarted_CurrentModeStartDrag()
        {
            var (input, actionMap) = SetupTest();

            input.Enable();
            BeginTouch(0, new());

            Assert.AreEqual(1, moveMode_dragStartedCount);
        }

        [Test]
        public void Enable_Drag_CurrentModeDrag()
        {
            var (input, actionMap) = SetupTest();

            input.Enable();
            BeginTouch(0, new(0, 0));
            MoveTouch(0, new(1, 1));

            Assert.AreEqual(1, moveMode_dragCount);
        }

        [Test]
        public void Enable_DragEnded_CurrentModeEndDrag()
        {
            var (input, actionMap) = SetupTest();

            input.Enable();
            BeginTouch(0, new());
            CancelTouch(0, new());

            Assert.AreEqual(1, moveMode_dragEndedCount);
        }

        [Test]
        public void Disable_Tap_NoChange()
        {
            var (input, actionMap) = SetupTest();

            input.Enable();
            input.Disable();
            BeginTouch(0, new());
            EndTouch(0, new());

            Assert.AreEqual(0, moveMode_tapCount);
        }

        [Test]
        public void Disable_DragStarted_NoChange()
        {
            var (input, actionMap) = SetupTest();

            input.Enable();
            input.Disable();
            BeginTouch(0, new());

            Assert.AreEqual(0, moveMode_dragStartedCount);
        }

        [Test]
        public void Disable_Drag_NoChange()
        {
            var (input, actionMap) = SetupTest();

            input.Enable();
            input.Disable();
            BeginTouch(0, new(0, 0));
            MoveTouch(0, new(1, 1));

            Assert.AreEqual(0, moveMode_dragCount);
        }

        [Test]
        public void Disable_DragEnded_NoChange()
        {
            var (input, actionMap) = SetupTest();

            input.Enable();
            input.Disable();
            BeginTouch(0, new());
            CancelTouch(0, new());

            Assert.AreEqual(0, moveMode_dragEndedCount);
        }

        [Test]
        public void SetInputMode_ToSelectMode_TapSwitched()
        {
            var (input, actionMap) = SetupTest();
            input.Enable();

            input.SetCurrentMode<ISelectInputMode>();
            BeginTouch(0, new());
            EndTouch(0, new());

            Assert.AreEqual(0, moveMode_tapCount);
            Assert.AreEqual(1, selectMode_tapCount);
        }

        [Test]
        public void SetInputMode_ToSelectMode_DragStartedSwitched()
        {
            var (input, actionMap) = SetupTest();
            input.Enable();

            input.SetCurrentMode<ISelectInputMode>();
            BeginTouch(0, new());

            Assert.AreEqual(0, moveMode_dragStartedCount);
            Assert.AreEqual(1, selectMode_dragStartedCount);
        }

        [Test]
        public void SetInputMode_ToSelectMode_DragSwitched()
        {
            var (input, actionMap) = SetupTest();
            input.Enable();

            input.SetCurrentMode<ISelectInputMode>();
            BeginTouch(0, new(0, 0));
            MoveTouch(0, new(1, 1));

            Assert.AreEqual(0, moveMode_dragCount);
            Assert.AreEqual(1, selectMode_dragCount);
        }

        [Test]
        public void SetInputMode_ToSelectMode_DragEndedSwitched()
        {
            var (input, actionMap) = SetupTest();
            input.Enable();

            input.SetCurrentMode<ISelectInputMode>();
            BeginTouch(0, new());
            CancelTouch(0, new());

            Assert.AreEqual(0, moveMode_dragEndedCount);
            Assert.AreEqual(1, selectMode_dragEndedCount);
        }

        [Test]
        public void GetInputMode_ExistingMode_ModeReturned()
        {
            var (input, actionMap) = SetupTest();
            input.Enable();

            var mode = input.GetInputMode<IMoveInputMode>();

            Assert.IsNotNull(mode);
        }

        [Test]
        public void GetInputMode_NotExistingMode_Null()
        {
            var (input, actionMap) = SetupTest();
            input.Enable();

            var mode = input.GetInputMode<IInputMode>();

            LogAssert.Expect(LogType.Error, $"Input doesn't contain {typeof(IInputMode).Name}");
            Assert.IsNull(mode);
        }
    }
}