using Model.Readonly;
using View;
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

        public IBlockView SpawnBlock(IBlock_Readonly blockModel)
        {
            IBlockView blockView = (IBlockView)Instantiate(blockPrefab.UnderlyingValue, parent);
            IBlockPresenter blockPresenter = new BlockPresenter(blockModel, blockView);
            blockPresenter.Init();
            blockView.Init(blockModel.Type, blockModel.Position);
            allBlocks.Add(blockPresenter);
            return blockView;
        }
        public Dictionary<IBlock_Readonly, IBlockView> SpawnGameBoard(IGameBoard_Readonly gameBoard)
        {
            Dictionary<IBlock_Readonly, IBlockView> spawnedBlocks = new();

            foreach (var blockModel in gameBoard.Blocks_Readonly)
            {
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