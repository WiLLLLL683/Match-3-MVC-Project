using UnityEngine;
using Utils;
using View;
using Config;
using Model.Objects;

namespace Presenter
{
    /// <summary>
    /// Контроллер для окна выбора уровня
    /// </summary>
    public class LevelSelectorPresenter : ISelectorPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<LevelProgress, ASelectorView, ISelectorPresenter>
        {
            private readonly Bootstrap bootstrap;
            private readonly LevelSO[] allLevels;

            public Factory(ASelectorView viewPrefab, Bootstrap bootstrap, LevelSO[] allLevels, Transform parent = null) : base(viewPrefab)
            {
                this.bootstrap = bootstrap;
                this.allLevels = allLevels;
            }

            public override ISelectorPresenter Connect(ASelectorView existingView, LevelProgress model)
            {
                var presenter = new LevelSelectorPresenter(model, existingView, bootstrap, allLevels);
                presenter.Enable();
                existingView.Init(allLevels[model.CurrentLevelIndex].icon, allLevels[model.CurrentLevelIndex].levelName);
                allPresenters.Add(presenter);
                return presenter;
            }
        }
        
        private LevelProgress model;
        private ASelectorView view;
        private Bootstrap bootstrap;
        private LevelSO[] allLevels;

        public LevelSelectorPresenter(LevelProgress model, ASelectorView view, Bootstrap bootstrap, LevelSO[] allLevels)
        {
            this.model = model;
            this.view = view;
            this.bootstrap = bootstrap;
            this.allLevels = allLevels;
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