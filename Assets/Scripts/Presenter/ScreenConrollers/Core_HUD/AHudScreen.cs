using Model.Infrastructure;
using Model.Objects;
using Model.Readonly;
using UnityEngine;
using View;
using Utils;

namespace Presenter
{
    public abstract class AHudScreen : AScreenController
    {
        public abstract void Init(ILevel_Readonly model,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory);

        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Экран создается только из Bootstrap.
        /// </summary>
        public static AHudScreen Create(AHudScreen prefab,
            ILevel_Readonly model,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> goalFactory,
            AFactory<ICounter_Readonly, ICounterView, ICounterPresenter> restrictionFactory)
        {
            var screen = GameObject.Instantiate(prefab);
            screen.Init(model, goalFactory, restrictionFactory);
            screen.Enable();
            return screen;
        }
    }
}