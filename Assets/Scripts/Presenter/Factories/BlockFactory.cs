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

        public override IBlockPresenter Connect(IBlockView existingView, IBlock_Readonly model)
        {
            var presenter = new BlockPresenter(model, existingView);
            presenter.Enable();
            existingView.Init(model.Type, model.Position);
            allPresenters.Add(presenter);
            return presenter;
        }

        public override IBlockView CreateView(IBlock_Readonly model, out IBlockPresenter presenter)
        {
            var view = GameObject.Instantiate(viewPrefab, parent);
            presenter = Connect(view, model);
            return view;
        }
    }
}
