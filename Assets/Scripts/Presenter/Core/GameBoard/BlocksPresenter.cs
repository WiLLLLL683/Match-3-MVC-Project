using Config;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Model.Objects;
using Model.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utils;
using View;
using View.Factories;
using View.Input;

namespace Presenter
{
    /// <summary>
    /// Презентер блоков на игровом поле
    /// Создает визуальные элементы блоков, а также синхронизирует их с моделью
    /// </summary>
    public class BlocksPresenter : IBlocksPresenter
    {
        private readonly Game model;
        private readonly IGameBoardView view;
        private readonly IBlockViewFactory blockViewFactory;
        private readonly IStateMachine stateMachine;
        private readonly IConfigProvider configProvider;
        private readonly IHudPresenter hud;
        private readonly IMoveInputMode moveInputMode;
        private readonly IBlockSpawnService spawnService;
        private readonly IBlockDestroyService destroyService;
        private readonly IBlockChangeTypeService changeTypeService;
        private readonly IBlockMoveService moveService;
        private readonly IWinLoseService winLoseService;

        private readonly Dictionary<Block, IBlockView> blocks = new();

        private GameBoard gameBoard;
        private IBlockView draggedBlock;
        private IBlockView oppositeBlock;
        private CancellationTokenSource tokenSource;

        public BlocksPresenter(Game model,
            IGameBoardView view,
            IBlockViewFactory blockViewFactory,
            IStateMachine stateMachine,
            IGameBoardInput input,
            IConfigProvider configProvider,
            IHudPresenter hud,
            IBlockSpawnService spawnService,
            IBlockDestroyService destroyService,
            IBlockChangeTypeService changeTypeService,
            IBlockMoveService moveService,
            IWinLoseService winLoseService)
        {
            this.model = model;
            this.view = view;
            this.blockViewFactory = blockViewFactory;
            this.stateMachine = stateMachine;
            this.moveInputMode = input.GetInputMode<IMoveInputMode>();
            this.configProvider = configProvider;
            this.hud = hud;
            this.spawnService = spawnService;
            this.destroyService = destroyService;
            this.changeTypeService = changeTypeService;
            this.moveService = moveService;
            this.winLoseService = winLoseService;
        }

        public void Enable()
        {
            tokenSource = new();
            gameBoard = model.CurrentLevel.gameBoard;
            SpawnAllViews();
            CenterGameBoard();

            moveInputMode.OnInputMove += OnInputMove;
            moveInputMode.OnInputActivate += OnInputActivate;
            moveInputMode.OnInputDrag += OnInputDrag;
            moveInputMode.OnInputRelease += OnInputRelease;
            spawnService.OnBlockSpawn += OnBlockSpawn;
            destroyService.OnDestroy += OnDestroy;
            moveService.OnPositionChange += OnPositionChange;
            moveService.OnFlyStarted += StartFly;
            changeTypeService.OnTypeChange += OnTypeChange;
        }

        public void Disable()
        {
            moveInputMode.OnInputMove -= OnInputMove;
            moveInputMode.OnInputActivate -= OnInputActivate;
            moveInputMode.OnInputDrag -= OnInputDrag;
            moveInputMode.OnInputRelease -= OnInputRelease;
            spawnService.OnBlockSpawn -= OnBlockSpawn;
            destroyService.OnDestroy -= OnDestroy;
            moveService.OnPositionChange -= OnPositionChange;
            moveService.OnFlyStarted -= StartFly;
            changeTypeService.OnTypeChange -= OnTypeChange;

            ClearAllViews();
            tokenSource.Cancel();
        }

        public IBlockView GetBlockView(Vector2Int modelPosition)
        {
            if (!gameBoard.Cells.IsInBounds(modelPosition))
                return null;

            Block blockModel = gameBoard.Cells[modelPosition.x, modelPosition.y].Block;
            if (blockModel == null || !blocks.ContainsKey(blockModel))
                return null;

            return blocks[blockModel];
        }

        //Event listeners
        private void OnInputMove(IBlockView blockView, Vector2 deltaPosition)
        {
            var (clampedDelta, direction) = ClampDeltaPosition(deltaPosition);
            stateMachine.EnterState<InputMoveBlockState, (Vector2Int, Directions)>((blockView.ModelPosition, direction));
        }

        private void OnInputActivate(IBlockView blockView)
        {
            if (blockView == null)
                return;

            blockView.PlayClickAnimation();
            stateMachine.EnterState<InputActivateBlockState, Vector2Int>(blockView.ModelPosition);
        }

        private void OnBlockSpawn(Block model)
        {
            IBlockView view = blockViewFactory.Create(model);
            blocks.Add(model, view);
        }

        private void OnDestroy(Block model)
        {
            if (winLoseService.TryGetGoal(model.Type, out Counter counter))
            {
                DestroyViewWithFlight(model, counter, tokenSource.Token).Forget();
            }
            else
            {
                DestroyView(model);
            }
        }

        private void OnInputDrag(IBlockView block, Vector2 deltaPosition)
        {
            draggedBlock = block;
            var (clampedDelta, direction) = ClampDeltaPosition(deltaPosition);

            if (TryGetOppositeBlock(direction))
            {
                draggedBlock?.Drag(clampedDelta);
                oppositeBlock?.Drag(-clampedDelta);
            }
            else
            {
                draggedBlock?.Release();
                oppositeBlock?.Release();
            }
        }

        private void OnInputRelease(IBlockView block)
        {
            draggedBlock?.Release();
            oppositeBlock?.Release();
            draggedBlock = null;
            oppositeBlock = null;
        }

        private void OnPositionChange(Block model)
        {
            if (model == null || !blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            view.SetModelPosition(model.Position);
        }

        private void OnTypeChange(Block model)
        {
            if (!blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            BlockTypeSO config = configProvider.GetBlockTypeSO(model.Type.Id);
            view.SetType(config.icon, config.typeConfig);
        }

        //private methods
        private void SpawnAllViews()
        {
            ClearAllViews();

            for (int i = 0; i < gameBoard.Blocks.Count; i++)
            {
                OnBlockSpawn(gameBoard.Blocks[i]);
            }
        }

        private void CenterGameBoard()
        {
            Vector2 offcet = new();
            offcet.x = -(float)(gameBoard.Cells.GetLength(0) - 1f) / 2f;
            offcet.y = -(float)(gameBoard.HiddenRowsStartIndex - 1f) / 2f - 1f;
            view.BlocksParent.position = offcet;
        }

        private void ClearAllViews()
        {
            for (int i = 0; i < gameBoard.Blocks.Count; i++)
            {
                DestroyView(gameBoard.Blocks[i]);
            }

            view.ClearBlocksParent();
            blocks.Clear();
        }

        private void DestroyView(Block model)
        {
            if (!blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            view.Destroy();
            blocks.Remove(model);
        }

        private async UniTask DestroyViewWithFlight(Block blockModel, Counter counter, CancellationToken token = default)
        {
            if (!blocks.ContainsKey(blockModel))
                return;

            IBlockView blockView = blocks[blockModel];
            blocks.Remove(blockModel);

            if(hud.TryGetCounterView(counter, out ICounterView counterView))
            {
                Vector2 worldPosition = counterView.gameObject.transform.position;
                Vector2 localPosition = view.BlocksParent.InverseTransformPoint(worldPosition);
                await blockView.FlyTo(localPosition, configProvider.Block.blockFlyDuration, token);
                blockView.Destroy();
            }
        }

        private void StartFly(Block model, Vector2Int targetPosition)
        {
            if (!blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            view.FlyTo(targetPosition, configProvider.Block.blockFlyDuration).Forget();
        }

        private (Vector2 clampedDelta, Directions direction) ClampDeltaPosition(Vector2 deltaPosition)
        {
            Directions direction = deltaPosition.ToDirection(configProvider.Input.minSwipeDistance);

            Vector2 clampedDelta = direction switch
            {
                Directions.Left => Vector2.ClampMagnitude(new(deltaPosition.x, 0), configProvider.Input.maxSwipeDistance),
                Directions.Right => Vector2.ClampMagnitude(new(deltaPosition.x, 0), configProvider.Input.maxSwipeDistance),
                Directions.Down => Vector2.ClampMagnitude(new(0, deltaPosition.y), configProvider.Input.maxSwipeDistance),
                Directions.Up => Vector2.ClampMagnitude(new(0, deltaPosition.y), configProvider.Input.maxSwipeDistance),
                Directions.Zero => Vector2.zero,
                _ => Vector2.zero
            };

            return (clampedDelta, direction);
        }

        private bool TryGetOppositeBlock(Directions direction)
        {
            if (direction == Directions.Zero)
                return false;

            IBlockView newOppositeBlock = GetBlockView(draggedBlock.ModelPosition + direction.ToVector2Int());

            if (newOppositeBlock == null)
                return false;

            if (oppositeBlock != newOppositeBlock)
            {
                oppositeBlock?.Release();
                oppositeBlock = newOppositeBlock;
            }

            return oppositeBlock != null;
        }
}
}