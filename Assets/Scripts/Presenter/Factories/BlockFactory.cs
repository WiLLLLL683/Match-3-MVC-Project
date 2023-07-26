using Model.Readonly;
using View;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;

namespace Presenter
{
    public class BlockFactory : AFactory<IBlock_Readonly, IBlockView, IBlockPresenter>
    {
        public BlockFactory(IBlockView viewPrefab, Transform parent = null) : base(viewPrefab, parent)
        {
        }

        public override IBlockView Create(IBlock_Readonly model, out IBlockPresenter presenter)
        {
            IBlockView view = GameObject.Instantiate(viewPrefab, parent);
            presenter = new BlockPresenter(model, view);
            presenter.Enable();
            view.Init(model.Type, model.Position);
            allPresenters.Add(presenter);
            return view;
        }
    }
}
