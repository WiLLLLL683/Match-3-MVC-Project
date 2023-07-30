using Model.Infrastructure;
using System;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class AEndGameScreen : AScreenController
    {
        public abstract void Init(Game game, AInput input);
        public abstract void OnLevelComplete();
        public abstract void OnDefeat();

        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Экран создается только из Bootstrap.
        /// </summary>
        public static AEndGameScreen Create(AEndGameScreen prefab, Game model, AInput input)
        {
            var screen = GameObject.Instantiate(prefab);
            screen.Init(model, input);
            screen.Enable();
            return screen;
        }
    }
}