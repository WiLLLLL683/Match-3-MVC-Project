using Model.Infrastructure;
using System;
using UnityEngine;

namespace Presenter
{
    public interface ILevelSelectionController
    {
        public GameObject gameObject { get; }

        void Init(Game game, Bootstrap bootstrap);
        void SelectNextLevel();
        void SelectPreviousLevel();
        void StartSelectedLevel();
    }
}