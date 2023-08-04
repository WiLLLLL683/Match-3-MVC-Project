using UnityEngine;
using Model.Readonly;
using Utils;
using View;

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
        public class Factory : AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter>
        {
            private readonly Bootstrap bootstrap;
            public Factory(ASelectorView viewPrefab, Bootstrap bootstrap, Transform parent = null) : base(viewPrefab)
            {
                this.bootstrap = bootstrap;
            }

            public override ISelectorPresenter Connect(ASelectorView existingView, ILevelSelection_Readonly model)
            {
                var presenter = new LevelSelectorPresenter(model, existingView, bootstrap);
                presenter.Enable();
                existingView.Init(model.CurrentLevelData.Icon, model.CurrentLevelData.LevelName);
                allPresenters.Add(presenter);
                return presenter;
            }
        }
        
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