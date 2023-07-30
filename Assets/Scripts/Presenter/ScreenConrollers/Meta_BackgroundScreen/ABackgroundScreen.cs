using System;
using UnityEngine;

namespace Presenter
{
    public abstract class ABackgroundScreen : AScreenController
    {
        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Инпут создается только из Bootstrap.
        /// </summary>
        public static ABackgroundScreen Create(ABackgroundScreen prefab)
        {
            var screen = GameObject.Instantiate(prefab);
            screen.gameObject.SetActive(true);
            return screen;
        }
    }
}