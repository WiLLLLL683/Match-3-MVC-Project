using UnityEngine;

namespace View
{
    /// <summary>
    /// Визуальный элемент клетки, вызывается из ICellPresenter
    /// </summary>
    public abstract class ACellView : MonoBehaviour
    {
        public abstract void Init(Vector2Int modelPosition, Sprite iconSprite, bool isPlayable, ParticleSystem destroyEffectPrefab, ParticleSystem emptyEffectPrefab);
        public abstract void ChangeType(Sprite iconSprite, bool isPlayable, ParticleSystem destroyEffectPrefab, ParticleSystem emptyEffectPrefab);
        public abstract void PlayDestroyEffect();
        public abstract void PlayEmptyEffect();
    }
}