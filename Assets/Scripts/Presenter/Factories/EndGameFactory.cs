using Model.Infrastructure;
using Presenter;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class EndGameFactory : AFactory<Game, AEndGameView, IEndGamePresenter>
    {
        private readonly IInput input;
        private readonly Bootstrap bootstrap;

        public EndGameFactory(AEndGameView viewPrefab,
            IInput input,
            Bootstrap bootstrap,
            Transform parent = null) : base(viewPrefab, parent)
        {
            this.input = input;
            this.bootstrap = bootstrap;
        }

        public override AEndGameView Create(Game model, out IEndGamePresenter presenter)
        {
            AEndGameView view = GameObject.Instantiate(viewPrefab, parent);
            presenter = new EndGamePresenter(model, view, input, bootstrap);
            presenter.Enable();
            view.Init();
            allPresenters.Add(presenter);
            return view;
        }
    }
}