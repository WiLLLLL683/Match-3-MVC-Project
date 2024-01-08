using Presenter;
using System;
using UnityEngine;
using Utils;

namespace View
{
    /// <summary>
    /// Выделенный функционал управления блоками на игровом поле
    /// </summary>
    public interface IInput
    {
        event Action<Vector2Int> OnInputSelect;
        event Action<Vector2Int> OnInputActivate;
        event Action<Vector2Int, Directions> OnInputMove;

        public abstract void EnableMoveInput();
        public abstract void DisableMoveInput();
    }
}