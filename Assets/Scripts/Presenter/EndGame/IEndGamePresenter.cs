using Model.Infrastructure;
using System;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IEndGamePresenter
    {
        public GameObject gameObject { get; }

        void Init(Game game, IInput input, Bootstrap bootstrap);
        void ShowCompleteMenu();
        void ShowDefeatMenu();
        void HideAllMenus();
        void Replay();
        void NextLevel();
        void Quit();
    }
}