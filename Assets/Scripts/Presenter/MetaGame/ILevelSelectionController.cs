using Model.Infrastructure;
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