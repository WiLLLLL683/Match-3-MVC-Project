using Model.Infrastructure;
using Model.Objects;
using Model.Readonly;
using UnityEngine;
using View;
using Utils;

namespace Presenter
{
    /// <summary>
    /// Контроллер для HUD
    /// </summary>
    public class HudPresenter : IHudPresenter
    {
        public class Factory : AFactory<ILevel_Readonly, AHudView, IHudPresenter>
        {
            private AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> goalFactory;
            private AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> restrictionFactory;
            public Factory(AHudView viewPrefab,
                AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> goalFactory,
                AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> restrictionFactory) : base(viewPrefab)
            {
                this.goalFactory = goalFactory;
                this.restrictionFactory = restrictionFactory;
            }

            public override IHudPresenter Connect(AHudView existingView, ILevel_Readonly model)
            {
                var presenter = new HudPresenter(model, existingView, goalFactory, restrictionFactory);
                presenter.Enable();
                allPresenters.Add(presenter);
                return presenter;
            }
        }
        
        private readonly ILevel_Readonly model;
        private readonly AHudView view;
        private readonly AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> goalFactory;
        private readonly AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> restrictionFactory;

        public HudPresenter(ILevel_Readonly model,
            AHudView view,
            AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> goalFactory,
            AFactory<ICounter_Readonly, ACounterView, ICounterPresenter> restrictionFactory)
        {
            this.model = model;
            this.view = view;
            this.goalFactory = goalFactory;
            this.goalFactory.SetParent(view.GoalsParent);
            this.restrictionFactory = restrictionFactory;
            this.restrictionFactory.SetParent(view.RestrictionsParent);
        }
        public void Enable()
        {
            //TODO
            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            //TODO
            Debug.Log($"{this} disabled");
        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }
    }
}