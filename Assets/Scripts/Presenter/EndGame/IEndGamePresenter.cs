using Model.Infrastructure;
using System;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IEndGamePresenter : IPresenter
    {
        void OnLevelComplete();
        void OnDefeat();
        void Replay();
        void NextLevel();
        void Quit();
    }
}