using Model.Infrastructure;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class APauseScreen : AScreenController
    {
        public abstract void Init(PlayerSettings settings, AInput input, Bootstrap bootstrap);

        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Экран создается только из Bootstrap.
        /// </summary>
        public static APauseScreen Create(APauseScreen prefab, PlayerSettings model, AInput input, Bootstrap bootstrap)
        {
            var pauseScreen = GameObject.Instantiate(prefab);
            pauseScreen.Init(model, input, bootstrap);
            pauseScreen.Enable();
            return pauseScreen;
        }
    }
}