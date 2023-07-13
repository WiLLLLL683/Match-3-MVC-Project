using System;
using UnityEngine;

namespace View
{
    /// <summary>
    /// Выделенный функционал управления блоками
    /// </summary>
    public interface IInput
    {
        public abstract void Enable();
        public abstract void Disable();
    }
}