using UnityEngine;
using Data;

namespace View
{
    /// <summary>
    /// Визуальный элемент клетки, вызывается из ICellPresenter
    /// </summary>
    public abstract class ICellView : MonoBehaviour
    {
        public abstract void Init(Vector2 modelPosition, ACellType type);
        public abstract void ChangeType(ACellType type);
        public abstract void PlayDestroyEffect();
        public abstract void PlayEmptyEffect();
    }
}