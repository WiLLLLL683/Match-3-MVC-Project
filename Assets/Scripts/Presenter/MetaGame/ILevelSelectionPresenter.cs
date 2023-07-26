using Model.Infrastructure;
using UnityEngine;

namespace Presenter
{
    public interface ILevelSelectionPresenter : IPresenter
    {
        void SelectNextLevel();
        void SelectPreviousLevel();
        void StartSelectedLevel();
    }
}