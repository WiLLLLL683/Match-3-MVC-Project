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
    }
}