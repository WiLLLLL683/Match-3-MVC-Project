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
        event Action<Vector2Int, Directions> OnInputMove;
        event Action<Vector2Int> OnInputActivate;
        event Action<IBlockView> OnInputRelease;
        event Action<IBlockView, Vector2> OnInputDrag;
    }
}