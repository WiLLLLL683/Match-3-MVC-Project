using UnityEngine;
using Presenter;
using Zenject;
using Utils;
using ModestTree.Util;
using System;

namespace View
{
    /// <summary>
    /// Тач инпут для мобильных телефонов.<br/>
    /// Перетаскивает вью блоков при свайпе по экрану, вызывает соответствующий метод инпута во вью блока при отпускании.<br/>
    /// Нажатия на экран отслеживаются в Update, выделение блока происходит через raycast, противоположный блок получается из IGameBoardPresenter
    /// </summary>
    public class Input_Touch : MonoBehaviour, IInput
    {
        [SerializeField] private float minSwipeDistance = 0.6f;
        [SerializeField] private float maxSwipeDistance = 1f;
        [SerializeField] private float tapDelay = 0.1f;

        public event Action<Vector2Int, Directions> OnInputMove;
        public event Action<Vector2Int> OnInputActivate;

        private IBlocksPresenter blocksPresenter;
        private Camera mainCamera;

        private IBlockView selectedBlock;
        private IBlockView oppositeBlock;
        private Vector2 firstTouchWorldPosition;
        private Vector2 deltaWorldPosition;
        private Vector2 deltaWorldPositionClamped;
        private Directions swipeDirection;
        private float timer;

        [Inject]
        public void Construct(IBlocksPresenter blocksPresenter)
        {
            this.blocksPresenter = blocksPresenter;
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
            }

            UpdateTimer();

            if (selectedBlock == null)
                return;

            if (touch.phase == TouchPhase.Moved)
            {
                GetDeltaWorldPosition(touch);
                GetSwipeDirection();
                if(TryGetOppositeBlock())
                    DragBlocks();
                else
                    ReleaseBlocks();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (timer > 0)
                    OnInputActivate?.Invoke(selectedBlock.ModelPosition);
                else
                    OnInputMove?.Invoke(selectedBlock.ModelPosition, swipeDirection);

                ReleaseBlocks();
                ClearSelection();
            }
        }

        public void Enable()
        {
            enabled = true;
            Debug.Log($"{this.GetType().Name} enabled");
        }

        public void Disable()
        {
            enabled = false;
            Debug.Log($"{this.GetType().Name} disabled");
        }

        private void ResetTimer() => timer = tapDelay;
        private void UpdateTimer() => timer -= Time.deltaTime;

        private bool TrySelectBlock(Touch touch)
        {
            Vector2 worldPoint = mainCamera.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider == null)
                return false;
            if (!hit.collider.TryGetComponent<IBlockView>(out IBlockView block))
                return false;

            firstTouchWorldPosition = worldPoint;
            selectedBlock = block;
            return true;
        }

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

        private bool TryGetOppositeBlock()
        {
            if (swipeDirection == Directions.Zero)
                return false;

            IBlockView newOppositeBlock = blocksPresenter.GetBlockView(selectedBlock.ModelPosition + swipeDirection.ToVector2Int());

            if (newOppositeBlock == null)
                return false;

            if (oppositeBlock != newOppositeBlock)
            {
                oppositeBlock?.Release();
                oppositeBlock = newOppositeBlock;
            }

            return oppositeBlock != null;
        }

        private void DragBlocks()
        {
            selectedBlock.Drag(deltaWorldPositionClamped);
            oppositeBlock?.Drag(-deltaWorldPositionClamped);
        }

        private void ReleaseBlocks()
        {
            selectedBlock.Release();
            oppositeBlock?.Release();
        }

        private void ClearSelection()
        {
            selectedBlock = null;
            oppositeBlock = null;
        }
    }
}