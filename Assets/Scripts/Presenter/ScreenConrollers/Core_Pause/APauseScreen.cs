using Model.Infrastructure;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    public abstract class APauseScreen : AScreenController
    {
        public abstract void Init(PlayerSettings settings, Game game, AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory, AInput input);

        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Экран создается только из Bootstrap.
        /// </summary>
        public static APauseScreen Create(APauseScreen prefab,
            PlayerSettings model,
            Game game,
            AFactory<PlayerSettings, APausePopUp, IPopUpPresenter> popUpFactory, 
            AInput input)
        {
            var pauseScreen = GameObject.Instantiate(prefab);
            pauseScreen.Init(model, game, popUpFactory, input);
            pauseScreen.Enable();
            return pauseScreen;
        }
    }
}