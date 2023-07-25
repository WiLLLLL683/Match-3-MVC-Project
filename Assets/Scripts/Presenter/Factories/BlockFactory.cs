using Model.Readonly;
using View;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace Presenter
{
    public class BlockFactory : FactoryBase<IBlock_Readonly, IBlockView>
    {
        private List<IBlockPresenter> allBlocks = new();

        public BlockFactory(IBlockView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override IBlockView Create(IBlock_Readonly model)
        {
            IBlockView view = GameObject.Instantiate(viewPrefab, parent);
            IBlockPresenter presenter = new BlockPresenter(model, view);
            presenter.Enable();
            view.Init(model.Type, model.Position);
            allBlocks.Add(presenter);
            return view;
        }
        public override void Clear()
        {
            for (int i = 0; i < allBlocks.Count; i++)
            {
                allBlocks[i].Destroy(null);
            }

            allBlocks.Clear();
        }
    }
}