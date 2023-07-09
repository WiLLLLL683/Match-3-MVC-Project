using Presenter;
using Data;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;
using AYellowpaper;

namespace Presenter
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private InterfaceReference<IBlockView, MonoBehaviour> blockPrefab;

        private List<IBlockPresenter> allBlocks = new();

        public IBlockPresenter SpawnBlock(Model.Objects.Block blockModel)
        {
            IBlockView blockView = (IBlockView)Instantiate(blockPrefab.UnderlyingValue, parent);
            IBlockPresenter blockPresenter = new BlockPresenter(blockModel, blockView);
            blockPresenter.Init();
            allBlocks.Add(blockPresenter);
            return blockPresenter;
        }
        public List<IBlockPresenter> SpawnGameBoard(Model.Objects.GameBoard gameBoard)
        {
            List<IBlockPresenter> spawnedBlocks = new();
            IBlockPresenter block;

            for (int x = 0; x < gameBoard.Blocks.Count; x++)
            {
                block = SpawnBlock(gameBoard.Blocks[x]);
                spawnedBlocks.Add(block);
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