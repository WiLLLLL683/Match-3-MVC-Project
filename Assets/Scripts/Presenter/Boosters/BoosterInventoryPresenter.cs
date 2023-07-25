using Data;
using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    /// <summary>
    /// Контроллер для инвентаря бустеров
    /// </summary>
    public class BoosterInventoryPresenter : MonoBehaviour, IBoosterInventoryPresenter
    {
        [SerializeField] private Transform boostersParent;

        private IFactory<IBooster, IBoosterView> factory;

        public void Init(Game game, PrefabConfig prefabs)
        {
            factory = new BoosterFactory(prefabs.boosterPrefab, boostersParent);
        }
    }
}