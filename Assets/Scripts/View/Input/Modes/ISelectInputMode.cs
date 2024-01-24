using System;
using UnityEngine;

namespace View.Input
{
    /// <summary>
    /// Режим ввода для выделения блоков игрового поля.
    /// </summary>
    public interface ISelectInputMode : IInputMode
    {
        event Action<Vector2Int> OnInputSelect;
    }
}