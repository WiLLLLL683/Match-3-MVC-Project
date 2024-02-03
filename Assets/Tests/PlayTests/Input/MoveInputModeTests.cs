using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

namespace View.Input.UnitTests
{
    public class MoveInputModeTests
    {
        private int inputActivateCount;
        private int inputDragCount;
        private int inputReleaseCount;
        private int inputMoveCount;

        private IMoveInputMode Setup()
        {
            inputActivateCount = 0;
            inputDragCount = 0;
            inputReleaseCount = 0;
            inputMoveCount = 0;

            var actionMap = new Match3ActionMap();
            actionMap.Enable();

            var camera = new GameObject().AddComponent<Camera>();
            camera.transform.position -= new Vector3(0, 0, -1);

            var inputMode = new MoveInputMode(actionMap, camera);
            inputMode.OnInputActivate += (_) => inputActivateCount++;
            inputMode.OnInputDrag += (_, _) => inputDragCount++;
            inputMode.OnInputRelease += (_) => inputReleaseCount++;
            inputMode.OnInputMove += (_, _) => inputMoveCount++;

            return inputMode;
        }

        [Test]
        public void Tap_BlockExists_ActivateEvent()
        {
            var inputMode = Setup();
            var block = new GameObject("Block", typeof(BlockView), typeof(BoxCollider2D));

            inputMode.Tap(new InputAction.CallbackContext());

            Assert.AreEqual(1, inputActivateCount);
            GameObject.DestroyImmediate(block);
        }

        [Test]
        public void Tap_NoBlock_NoActivateEvent()
        {
            var inputMode = Setup();

            inputMode.Tap(new InputAction.CallbackContext());

            Assert.AreEqual(0, inputActivateCount);
        }

        [Test]
        public void Drag_BlockExists_DragEvent()
        {
            var inputMode = Setup();
            var block = new GameObject("Block", typeof(BlockView), typeof(BoxCollider2D));

            inputMode.DragStarted(new InputAction.CallbackContext());
            inputMode.Drag(new InputAction.CallbackContext());

            Assert.AreEqual(1, inputDragCount);
            GameObject.DestroyImmediate(block);
        }

        [Test]
        public void Drag_NoBlock_NoDragEvent()
        {
            var inputMode = Setup();

            inputMode.DragStarted(new InputAction.CallbackContext());
            inputMode.Drag(new InputAction.CallbackContext());

            Assert.AreEqual(0, inputDragCount);
        }

        [Test]
        public void DragEnded_BlockExists_ReleaseEvent()
        {
            var inputMode = Setup();
            var block = new GameObject("Block", typeof(BlockView), typeof(BoxCollider2D));

            inputMode.DragStarted(new InputAction.CallbackContext());
            inputMode.Drag(new InputAction.CallbackContext());
            inputMode.DragEnded(new InputAction.CallbackContext());

            Assert.AreEqual(1, inputReleaseCount);
            GameObject.DestroyImmediate(block);
        }

        [Test]
        public void DragEnded_NoBlock_NoReleaseEvent()
        {
            var inputMode = Setup();

            inputMode.DragStarted(new InputAction.CallbackContext());
            inputMode.Drag(new InputAction.CallbackContext());
            inputMode.DragEnded(new InputAction.CallbackContext());

            Assert.AreEqual(0, inputReleaseCount);
        }

        [Test]
        public void DragEnded_BlockExists_MoveEvent()
        {
            var inputMode = Setup();
            var block = new GameObject("Block", typeof(BlockView), typeof(BoxCollider2D));

            inputMode.DragStarted(new InputAction.CallbackContext());
            inputMode.Drag(new InputAction.CallbackContext());
            inputMode.DragEnded(new InputAction.CallbackContext());

            Assert.AreEqual(1, inputMoveCount);
            GameObject.DestroyImmediate(block);
        }

        [Test]
        public void DragEnded_NoBlock_NoMoveEvent()
        {
            var inputMode = Setup();

            inputMode.DragStarted(new InputAction.CallbackContext());
            inputMode.Drag(new InputAction.CallbackContext());
            inputMode.DragEnded(new InputAction.CallbackContext());

            Assert.AreEqual(0, inputMoveCount);
        }
    }
}