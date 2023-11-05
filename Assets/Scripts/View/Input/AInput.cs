using Presenter;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Выделенный функционал управления блоками на игровом поле
    /// </summary>
    public abstract class AInput : MonoBehaviour
    {
        public abstract void Enable();
        public abstract void Disable();
    }
}