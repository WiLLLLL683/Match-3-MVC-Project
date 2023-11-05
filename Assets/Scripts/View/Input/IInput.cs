using Presenter;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Выделенный функционал управления блоками на игровом поле
    /// </summary>
    public interface IInput
    {
        public abstract void Enable();
        public abstract void Disable();
    }
}