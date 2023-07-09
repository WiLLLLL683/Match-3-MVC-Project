using Model.Infrastructure;
using UnityEngine;

namespace Presenter
{
    public interface IBoosterInventoryPresenter
    {
        public GameObject gameObject { get; }
        public void Init(Game game);
    }
}