using Model;
using System;
using System.Collections;
using UnityEngine;
using ViewElements;

namespace Controller
{
    public class Input_Touch : InputBase
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float minSwipeDistance = 0.1f;
        [SerializeField] private float maxSwipeDistance = 1f;
        [SerializeField] private float tapDelay = 0.1f;

        public override event Action<BlockView> OnTouchBegan;
        public override event Action<BlockView, Vector2> OnSwipeMoving;
        public override event Action<BlockView, Directions> OnSwipeEnded;
        public override event Action<BlockView> OnTap;

        private BlockView selectedBlock;
        private Vector2 firstTouchWorldPosition;
        private Vector2 deltaWorldPosition;
        private Vector2 deltaWorldPositionClamped;
        private Directions swipeDirection;
        private float timer;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount <= 0)
                return;

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (!TrySelectBlock(touch))
                    return;
                ResetTimer();
                OnTouchBegan?.Invoke(selectedBlock);
            }

            UpdateTimer();

            if (selectedBlock != null && touch.phase == TouchPhase.Moved)
            {
                GetDeltaWorldPosition(touch);
                GetSwipeDirection();
                OnSwipeMoving?.Invoke(selectedBlock, deltaWorldPositionClamped);
            }
            if (selectedBlock != null && touch.phase == TouchPhase.Ended)
            {
                if (timer > 0)
                    OnTap?.Invoke(selectedBlock);
                else
                    OnSwipeEnded?.Invoke(selectedBlock, swipeDirection);

                selectedBlock = null;
            }
        }
        public override void Enable() => enabled = true;
        public override void Disable() => enabled = false;

        private void ResetTimer() => timer = tapDelay;
        private void UpdateTimer() => timer -= Time.deltaTime;
        private void GetDeltaWorldPosition(Touch touch)
        {
            Vector2 worldPoint = mainCamera.ScreenToWorldPoint(touch.position);
            deltaWorldPosition = worldPoint - firstTouchWorldPosition;
        }
        private void GetSwipeDirection()
        {
            swipeDirection = Directions.Zero;
            deltaWorldPositionClamped = Vector2.zero;

            if (deltaWorldPosition.magnitude < minSwipeDistance)
                return;

            if (Mathf.Abs(deltaWorldPosition.x) > Mathf.Abs(deltaWorldPosition.y))
            {
                if (deltaWorldPosition.x > 0)
                {
                    swipeDirection = Directions.Right;
                    deltaWorldPositionClamped.Set(Mathf.Clamp(deltaWorldPosition.x, 0, maxSwipeDistance), 0);
                }
                else
                {
                    swipeDirection = Directions.Left;
                    deltaWorldPositionClamped.Set(Mathf.Clamp(deltaWorldPosition.x, -maxSwipeDistance, 0), 0);
                }
            }
            else
            {
                if (deltaWorldPosition.y > 0)
                {
                    swipeDirection = Directions.Up;
                    deltaWorldPositionClamped.Set(0, Mathf.Clamp(deltaWorldPosition.y, 0, maxSwipeDistance));
                }
                else
                {
                    swipeDirection = Directions.Down;
                    deltaWorldPositionClamped.Set(0, Mathf.Clamp(deltaWorldPosition.y, -maxSwipeDistance, 0));
                }
            }
        }
        private bool TrySelectBlock(Touch touch)
        {
            Vector2 worldPoint = mainCamera.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider == null)
                return false;
            if (!hit.collider.TryGetComponent<BlockView>(out BlockView block))
                return false;

            firstTouchWorldPosition = worldPoint;
            selectedBlock = block;
            return true;
        }

    }
}