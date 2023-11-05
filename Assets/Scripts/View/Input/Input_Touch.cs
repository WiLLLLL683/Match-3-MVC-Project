using UnityEngine;
using Presenter;
using Zenject;

namespace View
{
    /// <summary>
    /// Тач инпут для мобильных телефонов.<br/>
    /// Перетаскивает вью блоков при свайпе по экрану, вызывает соответствующий метод инпута во вью блока при отпускании.<br/>
    /// Нажатия на экран отслеживаются в Update, выделение блока происходит через raycast, противоположный блок получается из IGameBoardPresenter
    /// </summary>
    public class Input_Touch : AInput
    {
        [SerializeField] private float minSwipeDistance = 0.6f;
        [SerializeField] private float maxSwipeDistance = 1f;
        [SerializeField] private float tapDelay = 0.1f;

        private IBlockInput selectedBlock;
        private IBlockInput oppositeBlock;
        private Vector2 firstTouchWorldPosition;
        private Vector2 deltaWorldPosition;
        private Vector2 deltaWorldPositionClamped;
        private Directions swipeDirection;
        private float timer;

        private IGameBoardPresenter gameBoardPresenter;
        private Camera mainCamera;

        [Inject]
        public void Construct(IGameBoardPresenter gameBoardPresenter)
        {
            mainCamera = Camera.main;
            this.gameBoardPresenter = gameBoardPresenter;
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
                    selectedBlock.Input_ActivateBlock();
                else
                    selectedBlock.Input_MoveBlock(swipeDirection);

                ReleaseBlocks();
                ClearSelection();
            }
        }
        public override void Enable()
        {
            enabled = true;
            Debug.Log($"{this.GetType().Name} enabled");
        }
        public override void Disable()
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
            if (!hit.collider.TryGetComponent<IBlockInput>(out IBlockInput block))
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

            IBlockInput newOppositeBlock = (IBlockInput)gameBoardPresenter.GetBlockView(selectedBlock.ModelPosition + swipeDirection.ToVector2Int().ToViewPos());

            if (oppositeBlock != newOppositeBlock)
            {
                oppositeBlock?.Input_Release();
                oppositeBlock = newOppositeBlock;
            }

            return oppositeBlock != null;
        }
        private void DragBlocks()
        {
            selectedBlock.Input_Drag(swipeDirection, deltaWorldPositionClamped);
            oppositeBlock?.Input_Drag(swipeDirection.ToOpposite(), -deltaWorldPositionClamped);
        }
        private void ReleaseBlocks()
        {
            selectedBlock.Input_Release();
            oppositeBlock?.Input_Release();
        }
        private void ClearSelection()
        {
            selectedBlock = null;
            oppositeBlock = null;
        }
    }
}