using Presenter;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Выделенный функционал управления блоками на игровом поле
    /// </summary>
    public abstract class AInput : MonoBehaviour
    {
        public abstract AInput Init(IGameBoardPresenter gameBoardScreen);
        public abstract AInput Enable();
        public abstract AInput Disable();
    }
}