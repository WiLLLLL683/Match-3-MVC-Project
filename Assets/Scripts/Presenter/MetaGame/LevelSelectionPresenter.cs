using Data;
using Model.Infrastructure;
using Model.Readonly;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Контроллер для окна выбора уровня
    /// </summary>
    public class LevelSelectionPresenter : ILevelSelectionPresenter
    {
        private ILevelSelection_Readonly model;
        private ALevelSelectionView view;
        private Bootstrap bootstrap;

        public LevelSelectionPresenter(ILevelSelection_Readonly model, ALevelSelectionView view, Bootstrap bootstrap)
        {
            this.model = model;
            this.view = view;
            this.bootstrap = bootstrap;
        }
        public void Enable()
        {
            view.OnInputStart += StartSelectedLevel;
            view.OnInputNext += SelectNextLevel;
            view.OnInputPrevious += SelectPreviousLevel;
        }
        public void Disable()
        {
            view.OnInputStart -= StartSelectedLevel;
            view.OnInputNext -= SelectNextLevel;
            view.OnInputPrevious -= SelectPreviousLevel;
        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }

        public void SelectPreviousLevel()
        {
            //TODO
        }
        public void SelectNextLevel()
        {
            //TODO
        }
        public void StartSelectedLevel() => bootstrap.LoadCoreGame();
    }
}