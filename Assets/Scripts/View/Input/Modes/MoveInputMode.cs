using Config;
using Presenter;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Zenject;

namespace View.Input
{
    public class MoveInputMode : IMoveInputMode
    {
        public event Action<IBlockView, Vector2> OnInputMove;
        public event Action<IBlockView> OnInputActivate;
        public event Action<IBlockView, Vector2> OnInputDrag;
        public event Action<IBlockView> OnInputRelease;

        private readonly Match3ActionMap actionMap;
        private readonly Camera mainCamera;

        private Vector2 firstTouchWorldPoint;
        private IBlockView draggedBlock;
        private Vector2 worldDragDelta;

        public MoveInputMode(Match3ActionMap actionMap, Camera mainCamera)
        {
            this.actionMap = actionMap;
            this.mainCamera = mainCamera;
        }

        public void Tap(InputAction.CallbackContext context)
        {
            Vector2 worldPoint = GetWorldPoint();
            IBlockView block = TrySelectBlock(worldPoint);

            if (block == null)
                return;

            OnInputActivate?.Invoke(block);
        }

        public void DragStarted(InputAction.CallbackContext context)
        {
            firstTouchWorldPoint = GetWorldPoint();
            draggedBlock = TrySelectBlock(firstTouchWorldPoint);
        }

        public void Drag()
        {
            if (draggedBlock == null)
                return;

            worldDragDelta = GetWorldPoint() - firstTouchWorldPoint;
            OnInputDrag?.Invoke(draggedBlock, worldDragDelta);
        }

        public void DragEnded(InputAction.CallbackContext context)
        {
            if (draggedBlock == null)
                return;

            if (worldDragDelta.magnitude == 0)
                return;

            OnInputMove?.Invoke(draggedBlock, worldDragDelta);
            OnInputRelease?.Invoke(draggedBlock);
        }

        private Vector2 GetWorldPoint()
        {
            Vector2 screenPoint = actionMap.GameBoard.Point.ReadValue<Vector2>();
            return mainCamera.ScreenToWorldPoint(screenPoint);
        }

        private IBlockView TrySelectBlock(Vector2 worldPoint)
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider == null)
                return null;
            if (!hit.collider.TryGetComponent(out IBlockView block))
                return null;

            return block;
        }
    }
}