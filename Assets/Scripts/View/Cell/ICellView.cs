using UnityEngine;
using Data;

namespace View
{
    /// <summary>
    /// Визуальный элемент клетки, вызывается из ICellPresenter
    /// </summary>
    public interface ICellView
    {
        void Init(Vector2 modelPosition, ACellType type);
        void ChangeType(ACellType type);
        void PlayDestroyEffect();
        void PlayEmptyEffect();
    }
}