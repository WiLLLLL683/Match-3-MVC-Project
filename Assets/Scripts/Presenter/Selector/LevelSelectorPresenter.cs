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
    public class LevelSelectorPresenter : ISelectorPresenter
    {
        private ILevelSelection_Readonly model;
        private ASelectorView view;
        private Bootstrap bootstrap;

        public LevelSelectorPresenter(ILevelSelection_Readonly model, ASelectorView view, Bootstrap bootstrap)
        {
            this.model = model;
            this.view = view;
            this.bootstrap = bootstrap;
        }
        public void Enable()
        {
            view.OnStartSelected += StartSelected;
            view.OnSelectNext += SelectNext;
            view.OnSelectPrevious += SelectPrevious;

            view.gameObject.SetActive(true);
        }
        public void Disable()
        {
            view.OnStartSelected -= StartSelected;
            view.OnSelectNext -= SelectNext;
            view.OnSelectPrevious -= SelectPrevious;

            view.gameObject.SetActive(false);
        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }

        public void SelectPrevious()
        {
            //TODO
        }
        public void SelectNext()
        {
            //TODO
        }
        public void StartSelected() => bootstrap.LoadCoreGame();
    }
}