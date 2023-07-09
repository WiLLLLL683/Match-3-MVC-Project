using Presenter;
using Data;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private BlockView blockPrefab;

        private List<IBlockPresenter> allBlocks = new();

        public IBlockView SpawnBlock(Model.Objects.Block blockModel)
        {
            IBlockView blockView = Instantiate(blockPrefab, parent);
            IBlockPresenter blockController = new BlockPresenter(blockModel, blockView);
            blockController.Init();
            allBlocks.Add(blockController);
            return blockView;
        }
        public List<IBlockView> SpawnGameBoard(Model.Objects.GameBoard gameBoard)
        {
            List<IBlockView> spawnedBlocks = new();
            IBlockView block;

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