using Config;
using Presenter;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Zenject;

namespace View.Input
{
    public class MoveInputMode : IMoveInputMode
    {
        public event Action<IBlockView, Vector2> OnInputDrag;
        public event Action<IBlockView> OnInputRelease;
        public event Action<Vector2Int> OnInputActivate;
        public event Action<Vector2Int, Directions> OnInputMove;

        private readonly Match3ActionMap actionMap;
        private readonly Camera mainCamera;

        private Vector2 firstTouchWorldPoint;
        private IBlockView draggedBlock;
        private Directions direction;

        public MoveInputMode(Match3ActionMap actionMap)
        {
            this.actionMap = actionMap;
            this.mainCamera = Camera.main;
        }

        public void Tap(InputAction.CallbackContext context)
        {
            Vector2 worldPoint = GetWorldPoint();
            IBlockView block = TrySelectBlock(worldPoint);

            if (block == null)
                return;

            OnInputActivate?.Invoke(block.ModelPosition);
        }

        public void DragStarted(InputAction.CallbackContext context)
        {
            firstTouchWorldPoint = GetWorldPoint();
            draggedBlock = TrySelectBlock(firstTouchWorldPoint);
        }

        public void Drag(InputAction.CallbackContext context)
        {
            if (draggedBlock == null)
                return;

            Vector2 worldDelta = GetWorldPoint() - firstTouchWorldPoint;
            direction = worldDelta.ToDirection();
            OnInputDrag?.Invoke(draggedBlock, worldDelta);
        }

        public void DragEnded(InputAction.CallbackContext context)
        {
            if (draggedBlock == null)
                return;

            OnInputMove?.Invoke(draggedBlock.ModelPosition, direction);
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