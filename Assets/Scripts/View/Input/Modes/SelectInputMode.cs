using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace View.Input
{
    public class SelectInputMode : ISelectInputMode
    {
        public event Action<Vector2Int> OnInputSelect;

        private readonly Match3ActionMap actionMap;
        private readonly Camera mainCamera;

        public SelectInputMode(Match3ActionMap actionMap)
        {
            this.actionMap = actionMap;
            this.mainCamera = Camera.main;
        }

        public void Tap(InputAction.CallbackContext context)
        {
            IBlockView block = TrySelectBlock();

            if (block == null)
                return;

            OnInputSelect?.Invoke(block.ModelPosition);
        }

        public void DragStarted(InputAction.CallbackContext context) { }
        public void Drag(InputAction.CallbackContext context) { }
        public void DragEnded(InputAction.CallbackContext context) { }

        private IBlockView TrySelectBlock()
        {
            Vector2 screenPoint = actionMap.GameBoard.Point.ReadValue<Vector2>();
            Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider == null)
                return null;
            if (!hit.collider.TryGetComponent(out IBlockView block))
                return null;

            return block;
        }
    }
}