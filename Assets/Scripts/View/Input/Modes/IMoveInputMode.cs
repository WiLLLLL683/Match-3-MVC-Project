using System;
using UnityEngine;
using Utils;

namespace View.Input
{
    /// <summary>
    /// Режим ввода для перемещения и активации блоков.
    /// </summary>
    public interface IMoveInputMode : IInputMode
    {
        event Action<IBlockView, Vector2> OnInputMove;
        event Action<IBlockView> OnInputActivate;
        event Action<IBlockView, Vector2> OnInputDrag;
        event Action<IBlockView> OnInputRelease;
    }
}