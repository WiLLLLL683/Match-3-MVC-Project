using Data;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private Block blockPrefab;
        //[SerializeField] private AllBlockTypes allBlockTypes;

        private List<Block> allBlocks = new();

        public Block SpawnBlock(Model.Objects.Block _blockModel)
        {
            Block block = Instantiate(blockPrefab, parent);
            block.Init(_blockModel);
            allBlocks.Add(block);
            return block;
        }
        public List<Block> SpawnGameboard(Model.Objects.GameBoard gameBoard)
        {
            List<Block> spawnedBlocks = new();
            Block block;

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
                Destroy(allBlocks[i].gameObject);
            }

            //уничтожить неучтенные объекты
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}