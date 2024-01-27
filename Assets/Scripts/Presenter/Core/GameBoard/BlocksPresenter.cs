using Config;
using Infrastructure;
using Model.Objects;
using Model.Services;
using System;
using System.Collections.Generic;
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
        private readonly IConfigProvider configProvider;
        private readonly IBlockSpawnService spawnService;
        private readonly IBlockDestroyService destroyService;
        private readonly IBlockChangeTypeService changeTypeService;
        private readonly IBlockMoveService moveService;
        private readonly IStateMachine stateMachine;
        private readonly IGameBoardInput input;
        private readonly IMoveInputMode moveInputMode;

        private readonly Dictionary<Block, IBlockView> blocks = new();

        private GameBoard gameBoard;
        private IBlockView draggedBlock;
        private IBlockView oppositeBlock;

        public BlocksPresenter(Game model,
            IGameBoardView view,
            IBlockViewFactory blockViewFactory,
            IConfigProvider configProvider,
            IBlockSpawnService spawnService,
            IBlockDestroyService destroyService,
            IBlockChangeTypeService changeTypeService,
            IBlockMoveService moveService,
            IStateMachine stateMachine,
            IGameBoardInput input)
        {
            this.model = model;
            this.view = view;
            this.blockViewFactory = blockViewFactory;
            this.configProvider = configProvider;
            this.spawnService = spawnService;
            this.destroyService = destroyService;
            this.changeTypeService = changeTypeService;
            this.moveService = moveService;
            this.stateMachine = stateMachine;
            this.input = input;
            this.moveInputMode = input.GetInputMode<IMoveInputMode>();
        }

        public void Enable()
        {
            gameBoard = model.CurrentLevel.gameBoard;
            SpawnAll();
            CenterGameBoard();

            moveInputMode.OnInputDrag += DragView;
            moveInputMode.OnInputRelease += ReleaseView;
            moveInputMode.OnInputMove += MoveModel;
            moveInputMode.OnInputActivate += ActivateModel;
            spawnService.OnBlockSpawn += SpawnView;
            destroyService.OnDestroy += DestroyView;
            moveService.OnPositionChange += SetViewPosition;
            changeTypeService.OnTypeChange += SetViewType;

            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            moveInputMode.OnInputDrag -= DragView;
            moveInputMode.OnInputRelease -= ReleaseView;
            moveInputMode.OnInputMove -= MoveModel;
            moveInputMode.OnInputActivate -= ActivateModel;
            spawnService.OnBlockSpawn -= SpawnView;
            destroyService.OnDestroy -= DestroyView;
            moveService.OnPositionChange -= SetViewPosition;
            changeTypeService.OnTypeChange -= SetViewType;

            ClearAll();

            Debug.Log($"{this} disabled");
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

        private void SpawnAll()
        {
            ClearAll();

            for (int i = 0; i < gameBoard.Blocks.Count; i++)
            {
                SpawnView(gameBoard.Blocks[i]);
            }
        }

        private void ClearAll()
        {
            for (int i = 0; i < gameBoard.Blocks.Count; i++)
            {
                DestroyView(gameBoard.Blocks[i]);
            }

            view.ClearBlocksParent();
            blocks.Clear();
        }

        private void DragView(IBlockView block, Vector2 deltaPosition)
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

        private void ReleaseView(IBlockView block)
        {
            draggedBlock?.Release();
            oppositeBlock?.Release();
            draggedBlock = null;
            oppositeBlock = null;
        }

        private (Vector2 clampedDelta, Directions direction) ClampDeltaPosition(Vector2 deltaPosition)
        {
            if (deltaPosition.magnitude < configProvider.Input.minSwipeDistance)
                return (Vector2.zero, Directions.Zero);

            Directions direction = deltaPosition.ToDirection();

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

        private void MoveModel(Vector2Int position, Directions direction) =>
            stateMachine.EnterState<InputMoveBlockState, (Vector2Int, Directions)>((position, direction));

        private void ActivateModel(Vector2Int position)
        {
            IBlockView blockView = GetBlockView(position);

            if (blockView == null)
                return;

            blockView.PlayClickAnimation();
            stateMachine.EnterState<InputActivateBlockState, Vector2Int>(blockView.ModelPosition);
        }

        private void SpawnView(Block model)
        {
            IBlockView view = blockViewFactory.Create(model);
            blocks.Add(model, view);
        }

        private void DestroyView(Block model)
        {
            if (!blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            view.Destroy();
            blocks.Remove(model);
        }

        private void SetViewPosition(Block model)
        {
            if (model == null || !blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            view.SetModelPosition(model.Position);
        }

        private void SetViewType(Block model)
        {
            if (!blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            BlockTypeSO config = configProvider.GetBlockTypeSO(model.Type.Id);
            view.SetType(config.icon, config.destroyEffect);
        }

        private void CenterGameBoard()
        {
            Vector2 offcet = new();
            offcet.x = -(float)(gameBoard.Cells.GetLength(0) - 1f) / 2f;
            offcet.y = -(float)(gameBoard.HiddenRowsStartIndex - 1f) / 2f -1f;
            view.BlocksParent.position = offcet;
        }
    }
}