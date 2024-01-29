using Presenter;
using System;
using UnityEngine;
using Utils;
using View.Input;

namespace View
{
    /// <summary>
    /// Выделенный функционал управления блоками на игровом поле с возможностью переключать режим ввода.
    /// </summary>
    public interface IGameBoardInput
    {
        void Enable();
        void Disable();

        /// <summary>
        /// Задать текущий режим ввода.
        /// Тип Т должен быть интерфейсом (конвенция для обеспечения тестируемости).
        /// </summary>
        void SetCurrentMode<T>() where T : IInputMode;

        /// <summary>
        /// Получить режим ввода заданного типа.
        /// Тип Т должен быть интерфейсом (конвенция для обеспечения тестируемости).
        /// </summary>
        T GetInputMode<T>() where T : IInputMode;
    }
}