using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace View.Input.UnitTests
{
    public class SelectInputModeTests
    {
        private int inputSelectCount;

        private ISelectInputMode Setup()
        {
            inputSelectCount = 0;

            var actionMap = new Match3ActionMap();
            actionMap.Enable();

            var camera = new GameObject().AddComponent<Camera>();
            camera.transform.position -= new Vector3(0, 0, -1);

            var inputMode = new SelectInputMode(actionMap, camera);
            inputMode.OnInputSelect += (_) => inputSelectCount++;

            return inputMode;
        }

        [Test]
        public void Tap_BlockExists_SelectEvent()
        {
            var inputMode = Setup();
            var block = new GameObject("Block", typeof(BlockView), typeof(BoxCollider2D));

            inputMode.Tap(new InputAction.CallbackContext());

            Assert.AreEqual(1, inputSelectCount);
            GameObject.DestroyImmediate(block);
        }

        [Test]
        public void Tap_NoBlock_NoSelectEvent()
        {
            var inputMode = Setup();

            inputMode.Tap(new InputAction.CallbackContext());

            Assert.AreEqual(0, inputSelectCount);
        }
    }
}