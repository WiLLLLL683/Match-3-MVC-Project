using Data;
using Model.Objects;
using View;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace Presenter
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private InterfaceReference<IBlockView, MonoBehaviour> blockPrefab;

        private List<IBlockPresenter> allBlocks = new();
        private IGameBoardPresenter gameBoardPresenter;

        public void Init(IGameBoardPresenter gameBoardPresenter)
        {
            this.gameBoardPresenter = gameBoardPresenter;
        }
        public IBlockView SpawnBlock(Block blockModel)
        {
            IBlockView blockView = (IBlockView)Instantiate(blockPrefab.UnderlyingValue, parent);
            IBlockPresenter blockPresenter = new BlockPresenter(blockModel, blockView);
            blockPresenter.Init();
            blockView.Init(blockModel.Type, blockModel.Position, gameBoardPresenter);
            allBlocks.Add(blockPresenter);
            return blockView;
        }
        public Dictionary<Block, IBlockView> SpawnGameBoard(GameBoard gameBoard)
        {
            Dictionary<Block, IBlockView> spawnedBlocks = new();

            for (int i = 0; i < gameBoard.Blocks.Count; i++)
            {
                Block blockModel = gameBoard.Blocks[i];
                spawnedBlocks[blockModel] = SpawnBlock(blockModel);
            }

            return spawnedBlocks;
        }
        public void Clear()
        {
            for (int i = 0; i < allBlocks.Count; i++)
            {
                allBlocks[i].Destroy(null);
            }

            allBlocks.Clear();

            //уничтожить неучтенные объекты
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}