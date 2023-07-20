using Presenter;

namespace View
{
    /// <summary>
    /// Выделенный функционал управления блоками на игровом поле
    /// </summary>
    public interface IInput
    {
        void Init(IGameBoardPresenter gameBoardPresenter);
        public abstract void Enable();
        public abstract void Disable();
    }
}