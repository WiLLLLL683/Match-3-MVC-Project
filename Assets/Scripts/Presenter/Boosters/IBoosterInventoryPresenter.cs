using Data;
using Model.Infrastructure;
using UnityEngine;

namespace Presenter
{
    public interface IBoosterInventoryPresenter
    {
        public void Init(Game game, PrefabConfig prefabs);

        public GameObject gameObject { get; }
    }
}