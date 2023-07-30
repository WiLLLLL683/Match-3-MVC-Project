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
    public class HudScreen : AHudScreen
    {
        [SerializeField] private Transform goalsParent;
        [SerializeField] private Transform restrictionsParent;
        
        private ILevel_Readonly model;
        private AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory;
        private AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory;

        public override void Init(ILevel_Readonly model,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory)
        {
            this.model = model;
            this.goalFactory = goalFactory;
            this.goalFactory.SetParent(this.goalsParent);
            this.restrictionFactory = restrictionFactory;
            this.restrictionFactory.SetParent(this.restrictionsParent);
        }
        public override void Enable()
        {
            //TODO
            Debug.Log($"{this} enabled", this);
        }
        public override void Disable()
        {
            //TODO
            Debug.Log($"{this} disabled", this);
        }
    }
}