using UnityEngine;
using Data;

namespace View
{
    /// <summary>
    /// Визуальный элемент клетки.<br/>
    /// При инициализации выбирает цвет своей заливки для создания шахматного рисунка игрового поля.
    /// Может изменять свой тип и проигрывать эффекты.
    /// </summary>
    public class CellView : ACellView
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private SpriteRenderer fill;
        [SerializeField] private Sprite evenSprite;
        [SerializeField] private Sprite oddSprite;

        private Vector2 modelPosition;
        private ParticleSystem destroyEffectPrefab;
        private ParticleSystem emptyEffectPrefab;
        private ParticleSystem destroyEffect;
        private ParticleSystem emptyEffect;

        public override void Init(Vector2Int modelPosition, Sprite iconSprite, bool isPlayable, ParticleSystem destroyEffectPrefab, ParticleSystem emptyEffectPrefab)
        {
            ChangeModelPosition(modelPosition);
            ChangeType(iconSprite, isPlayable, destroyEffectPrefab, emptyEffectPrefab);
            SetCheckerBoardPattern();
        }

        public override void ChangeType(Sprite iconSprite, bool isPlayable, ParticleSystem destroyEffectPrefab, ParticleSystem emptyEffectPrefab)
        {
            this.destroyEffectPrefab = destroyEffectPrefab;
            this.emptyEffectPrefab = emptyEffectPrefab;

            if (iconSprite != null)
            {
                icon.gameObject.SetActive(true);
                icon.sprite = iconSprite;
            }
        }

        public override void PlayDestroyEffect()
        {
            if (destroyEffectPrefab == null)
                return;

            if (destroyEffect == null)
                destroyEffect = Instantiate(destroyEffectPrefab, transform);

            destroyEffect.Play();
        }

        public override void PlayEmptyEffect()
        {
            if (emptyEffectPrefab == null)
                return;

            if (emptyEffect == null)
                emptyEffect = Instantiate(emptyEffectPrefab, transform);

            emptyEffect.Play();
        }



        private void ChangeModelPosition(Vector2Int modelPosition)
        {
            this.modelPosition = modelPosition;
            transform.localPosition = (Vector2)modelPosition.ToViewPos();
        }
        private void SetCheckerBoardPattern()
        {
            if (evenSprite == null || oddSprite == null)
                return;

            if ((modelPosition.x % 2 == 1 && modelPosition.y % 2 == 1) ||
                (modelPosition.x % 2 == 0 && modelPosition.y % 2 == 0))
                fill.sprite = evenSprite;
            else
                fill.sprite = oddSprite;
        }
    }
}