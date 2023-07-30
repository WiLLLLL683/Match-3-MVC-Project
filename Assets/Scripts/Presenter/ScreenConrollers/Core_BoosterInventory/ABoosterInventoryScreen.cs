using Data;
using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class ABoosterInventoryScreen : AScreenController
    {
        public abstract void Init(BoosterInventory model,
            AFactory<IBooster, IBoosterView, IBoosterPresenter> factory);
    }
}