using UnityEngine;
using Utils;

namespace View
{
    /// <summary>
    /// Методы инпута вызываемые из IInput
    /// </summary>
    public interface IBlockInput
    {
        Vector2Int ModelPosition { get; }

        public void Input_MoveBlock(Directions direction);
        public void Input_ActivateBlock();
        public void Input_Drag(Directions direction, Vector2 deltaPosition);
        public void Input_Release();
    }
}