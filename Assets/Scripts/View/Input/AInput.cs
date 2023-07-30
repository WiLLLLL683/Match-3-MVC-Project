using Presenter;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Выделенный функционал управления блоками на игровом поле
    /// </summary>
    public abstract class AInput : MonoBehaviour
    {
        public abstract void Init(AGameBoardScreen gameBoardScreen);
        public abstract void Enable();
        public abstract void Disable();

        /// <summary>
        /// Factory-method
        /// Находится здесь, так как нет необходимости передавать объект фабрики как зависимость.
        /// Инпут создается только из Bootstrap.
        /// </summary>
        public static AInput Create(AInput prefab, AGameBoardScreen gameBoardScreen)
        {
            AInput input = GameObject.Instantiate(prefab);
            input.Init(gameBoardScreen);
            input.Enable();
            return input;
        }
    }
}