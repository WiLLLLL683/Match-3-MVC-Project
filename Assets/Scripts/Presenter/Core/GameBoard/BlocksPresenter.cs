﻿using Config;
using Infrastructure;
using Model.Objects;
using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using View;
using View.Factories;

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

        private readonly Dictionary<Block, IBlockView> blocks = new();

        private GameBoard gameBoard;

        public BlocksPresenter(Game model,
            IGameBoardView view,
            IBlockViewFactory blockViewFactory,
            IConfigProvider configProvider,
            IBlockSpawnService spawnService,
            IBlockDestroyService destroyService,
            IBlockChangeTypeService changeTypeService,
            IBlockMoveService moveService,
            IStateMachine stateMachine)
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
        }

        public void Enable()
        {
            gameBoard = model.CurrentLevel.gameBoard;
            SpawnAll();
            CenterGameBoard();

            spawnService.OnBlockSpawn += Spawn;
            destroyService.OnDestroy += Destroy;
            moveService.OnPositionChange += SyncPosition;
            changeTypeService.OnTypeChange += ChangeType;

            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            ClearAll();

            spawnService.OnBlockSpawn -= Spawn;
            destroyService.OnDestroy -= Destroy;
            moveService.OnPositionChange -= SyncPosition;
            changeTypeService.OnTypeChange -= ChangeType;

            Debug.Log($"{this} disabled");
        }

        public IBlockView GetBlockView(Vector2Int modelPosition)
        {
            Block blockModel = gameBoard.Cells[modelPosition.x, modelPosition.y].Block;
            if (blockModel == null || !blocks.ContainsKey(blockModel))
                return null;

            return blocks[blockModel];
        }

        public void InputMove(Vector2Int position, Directions direction)
        {
            stateMachine.EnterState<InputMoveBlockState, (Vector2Int, Directions)>((position, direction));
        }

        public void InputActivate(Vector2Int position)
        {
            IBlockView view = GetBlockView(position);
            if (view == null)
                return;

            view.PlayClickAnimation();
            stateMachine.EnterState<InputActivateBlockState, Vector2Int>(position);
        }

        private void SpawnAll()
        {
            ClearAll();

            for (int i = 0; i < gameBoard.Blocks.Count; i++)
            {
                Spawn(gameBoard.Blocks[i]);
            }
        }

        private void ClearAll()
        {
            for (int i = 0; i < gameBoard.Blocks.Count; i++)
            {
                Destroy(gameBoard.Blocks[i]);
            }

            foreach (Transform block in view.BlocksParent)
            {
                GameObject.Destroy(block.gameObject);
            }

            blocks.Clear();
        }

        private void Spawn(Block model)
        {
            IBlockView view = blockViewFactory.Create(model);
            blocks.Add(model, view);
            view.OnInputMove += InputMove;
            view.OnInputActivate += InputActivate;
        }

        private void Destroy(Block model)
        {
            if (!blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            view.PlayDestroyEffect();
            view.OnInputMove -= InputMove;
            view.OnInputActivate -= InputActivate;
            GameObject.Destroy(view.gameObject);
            blocks.Remove(model);
        }

        private void SyncPosition(Block model)
        {
            if (model == null || !blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            view.ChangeModelPosition(model.Position);
        }

        private void ChangeType(Block model)
        {
            if (!blocks.ContainsKey(model))
                return;

            IBlockView view = blocks[model];
            BlockTypeSO config = configProvider.GetBlockTypeSO(model.Type.Id);
            view.ChangeType(config.icon, config.destroyEffect);
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