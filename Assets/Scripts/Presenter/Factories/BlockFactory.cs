using Model.Readonly;
using View;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace Presenter
{
    public class BlockFactory : IFactory<IBlock_Readonly, IBlockView>
    {
        private Transform parent;
        private IBlockView blockPrefab;

        private List<IBlockPresenter> allBlocks = new();

        public BlockFactory(IBlockView viewPrefab, Transform parent)
        {
            this.blockPrefab = viewPrefab;
            this.parent = parent;
        }
        public IBlockView Create(IBlock_Readonly blockModel)
        {
            IBlockView blockView = GameObject.Instantiate(blockPrefab, parent);
            IBlockPresenter blockPresenter = new BlockPresenter(blockModel, blockView);
            blockPresenter.Init();
            blockView.Init(blockModel.Type, blockModel.Position);
            allBlocks.Add(blockPresenter);
            return blockView;
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
                GameObject.Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}