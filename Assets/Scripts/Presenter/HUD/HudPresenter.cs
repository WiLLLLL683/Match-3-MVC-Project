using Model.Infrastructure;
using Model.Objects;
using Model.Readonly;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Контроллер для HUD
    /// </summary>
    public class HudPresenter : IHudPresenter
    {
        private ILevel_Readonly model;
        private AHudView view;
        private AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory;
        private AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory;

        public HudPresenter(ILevel_Readonly model,
            AHudView view,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory)
        {
            this.model = model;
            this.view = view;
            this.goalFactory = goalFactory;
            this.goalFactory.SetParent(this.view.GoalsParent);
            this.restrictionFactory = restrictionFactory;
            this.restrictionFactory.SetParent(this.view.RestrictionsParent);
        }
        public void Enable()
        {

        }
        public void Disable()
        {

        }
        public void Destroy()
        {
            Disable();
            GameObject.Destroy(view.gameObject);
        }
    }
}