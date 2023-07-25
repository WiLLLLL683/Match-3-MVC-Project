using Presenter;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Выделенный функционал управления блоками на игровом поле
    /// </summary>
    public interface IInput
    {
        GameObject gameObject { get; }

        void Init(IGameBoardPresenter gameBoardPresenter);
        public abstract void Enable();
        public abstract void Disable();
    }
}