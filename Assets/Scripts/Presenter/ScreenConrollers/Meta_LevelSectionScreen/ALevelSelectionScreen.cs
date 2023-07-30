using Model.Infrastructure;
using Model.Objects;
using Model.Readonly;
using UnityEngine;
using View;
using Utils;

namespace Presenter
{
    public abstract class ALevelSelectionScreen : AScreenController
    {
        public abstract void Init(ILevelSelection_Readonly model,
                         AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory);

        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Инпут создается только из Bootstrap.
        /// </summary>
        public static ALevelSelectionScreen Create(ALevelSelectionScreen prefab, LevelSelection levelSelection, AFactory<ILevelSelection_Readonly, ASelectorView, ISelectorPresenter> selectorFactory)
        {
            var levelSelectionScreen = GameObject.Instantiate(prefab);
            levelSelectionScreen.Init(levelSelection, selectorFactory);
            levelSelectionScreen.Enable();
            return levelSelectionScreen;
        }
    }
}