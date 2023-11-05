using UnityEngine;

namespace View
{
    /// <summary>
    /// Визуальный элемент клетки, вызывается из ICellPresenter
    /// </summary>
    public interface ICellView
    {
        public GameObject gameObject { get; }

        public void Init(Vector2Int modelPosition, Sprite iconSprite, bool isPlayable, ParticleSystem destroyEffectPrefab, ParticleSystem emptyEffectPrefab);
        public void ChangeType(Sprite iconSprite, bool isPlayable, ParticleSystem destroyEffectPrefab, ParticleSystem emptyEffectPrefab);
        public void PlayDestroyEffect();
        public void PlayEmptyEffect();
    }
}