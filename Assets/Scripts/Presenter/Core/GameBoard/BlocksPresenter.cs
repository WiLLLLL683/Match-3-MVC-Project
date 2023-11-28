using CompositionRoot;
using Config;
using Model.Objects;
using Model.Services;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;
using View.Factories;
using Zenject;

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
        private readonly IBlockSpawnService blockSpawnService;
        private readonly BlockPresenter.Factory blockPresenterFactory;
        private readonly BlockView.Factory blockViewFactory;
        private readonly BlockTypeSetSO blockTypeSet;

        private readonly Dictionary<Block, IBlockView> blocks = new();

        private GameBoard gameBoard;

        public BlocksPresenter(Game model,
            IGameBoardView view,
            IBlockSpawnService blockSpawnService,
            BlockPresenter.Factory blockPresenterFactory,
            BlockView.Factory blockViewFactory,
            BlockTypeSetSO blockTypeSet)
        {
            this.model = model;
            this.view = view;
            this.blockSpawnService = blockSpawnService;
            this.blockPresenterFactory = blockPresenterFactory;
            this.blockViewFactory = blockViewFactory;
            this.blockTypeSet = blockTypeSet;
        }

        public void Enable()
        {
            gameBoard = model.CurrentLevel.gameBoard;
            SpawnAllBlocks();

            blockSpawnService.OnBlockSpawn += SpawnBlock;

            Debug.Log($"{this} enabled");
        }

        public void Disable()
        {
            blockSpawnService.OnBlockSpawn -= SpawnBlock;

            Debug.Log($"{this} disabled");
        }

        public IBlockView GetBlockView(Vector2Int modelPosition)
        {
            Block blockModel = gameBoard.Cells[modelPosition.x, modelPosition.y].Block;
            if (blockModel == null || !blocks.ContainsKey(blockModel))
                return null;

            return blocks[blockModel];
        }

        [Button]
        private void SpawnAllBlocks()
        {
            ClearAllBlocks();

            foreach (var blockModel in gameBoard.Blocks)
            {
                SpawnBlock(blockModel);
            }
        }

        private void ClearAllBlocks()
        {
            foreach (Transform block in view.BlocksParent)
            {
                GameObject.Destroy(block.gameObject);
            }

            blocks.Clear();
        }

        private void SpawnBlock(Block blockModel)
        {
            IBlockView blockView = blockViewFactory.Create();
            BlockTypeSO blockTypeSO = blockTypeSet.GetSO(blockModel.Type.Id);
            IBlockPresenter blockPresenter = blockPresenterFactory.Create(blockModel, blockView, blockTypeSO, blockTypeSet);
            blockPresenter.Enable();
            blocks.Add(blockModel, blockView);
        }
    }
}