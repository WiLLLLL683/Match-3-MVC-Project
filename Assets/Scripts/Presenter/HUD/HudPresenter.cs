using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Контроллер для HUD
    /// </summary>
    public class HudPresenter : MonoBehaviour, IHudPresenter
    {
        [SerializeField] private Transform goalsParent;
        [SerializeField] private Transform restrictionsParent;
        
        private Game game;
        private FactoryBase<Counter, ICounterView> goalFactory;
        private FactoryBase<Counter, ICounterView> restrictionFactory;

        public void Init(Game game,
            FactoryBase<Counter, ICounterView> goalFactory,
            FactoryBase<Counter, ICounterView> restrictionFactory)
        {
            this.game = game;
            this.goalFactory = goalFactory;
            this.goalFactory.SetParent(goalsParent);
            this.restrictionFactory = restrictionFactory;
            this.restrictionFactory.SetParent(restrictionsParent);
        }
        public void Enable()
        {

        }
        public void Disable()
        {

        }
    }
}