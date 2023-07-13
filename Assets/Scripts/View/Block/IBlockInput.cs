using UnityEngine;

namespace View
{
    /// <summary>
    /// Методы вызываемые из IInput
    /// </summary>
    public interface IBlockInput
    {
        public void Input_MoveBlock(Directions direction);
        public void Input_ActivateBlock();
        public void Input_Drag(Directions direction, Vector2 deltaPosition);
    }
}