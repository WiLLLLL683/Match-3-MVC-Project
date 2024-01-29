using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace View.Input
{
    /// <summary>
    /// Режим ввода игрового поля, вызывается из IGameBoardInput
    /// </summary>
    public interface IInputMode
    {
        void Tap(InputAction.CallbackContext context);
        void DragStarted(InputAction.CallbackContext context);
        void Drag(InputAction.CallbackContext context);
        void DragEnded(InputAction.CallbackContext context);
    }
}