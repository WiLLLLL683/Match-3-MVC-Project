using Data;
using Model.Infrastructure;
using Model.Objects;
using UnityEngine;
using View;
using Utils;

namespace Presenter
{
    public abstract class ABoosterInventoryScreen : AScreenController
    {
        public abstract void Init(BoosterInventory model,
            AFactory<IBooster, IBoosterView, IBoosterPresenter> factory);

        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Экран создается только из Bootstrap.
        /// </summary>
        public static ABoosterInventoryScreen Create(ABoosterInventoryScreen prefab,
            BoosterInventory model,
            AFactory<IBooster, IBoosterView, IBoosterPresenter> factory)
        {
            var screen = GameObject.Instantiate(prefab);
            screen.Init(model, factory);
            screen.Enable();
            return screen;
        }
    }
}