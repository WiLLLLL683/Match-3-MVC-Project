using Model.Infrastructure;
using Model.Readonly;
using System;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public abstract class AEndGameScreen : AScreenController
    {
        public abstract void Init(ILevel_Readonly model,
            AInput input,
            AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> factory);
        public abstract void ShowCompletePopUp();
        public abstract void ShowDefeatPopUp();

        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Экран создается только из Bootstrap.
        /// </summary>
        public static AEndGameScreen Create(AEndGameScreen prefab,
            ILevel_Readonly model,
            AInput input,
            AFactory<ILevel_Readonly, AEndGamePopUp, IPopUpPresenter> factory)
        {
            var screen = GameObject.Instantiate(prefab);
            screen.Init(model, input, factory);
            screen.Enable();
            return screen;
        }
    }
}