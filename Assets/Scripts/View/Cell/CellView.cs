using UnityEngine;
using Data;

namespace View
{
    /// <summary>
    /// Визуальный элемент клетки.<br/>
    /// При инициализации выбирает цвет своей заливки для создания шахматного рисунка игрового поля.
    /// Может изменять свой тип и проигрывать эффекты.
    /// </summary>
    public class CellView : MonoBehaviour, ICellView
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private SpriteRenderer fill;
        [SerializeField] private Sprite evenSprite;
        [SerializeField] private Sprite oddSprite;

        private Vector2 modelPosition;
        private ACellType type;
        private ParticleSystem destroyEffect;
        private ParticleSystem emptyEffect;

        public void Init(Vector2 modelPosition, ACellType type)
        {
            this.modelPosition = modelPosition;

            transform.localPosition = ModelPosToViewPos(modelPosition);
            ChangeType(type);
            SetCheckerBoardPattern();
        }
        public void PlayDestroyEffect()
        {
            if (type.DestroyEffect == null)
                return;

            if (destroyEffect == null)
                destroyEffect = Instantiate(type.DestroyEffect, transform);

            destroyEffect.Play();
        }
        public void PlayEmptyEffect()
        {
            if (type.EmptyEffect == null)
                return;

            if (emptyEffect == null)
                emptyEffect = Instantiate(type.EmptyEffect, transform);

            emptyEffect.Play();
        }
        public void ChangeType(ACellType type)
        {
            this.type = type;

            if (!type.IsPlayable)
            {
                gameObject.SetActive(false);
                return;
            }

            if (type.Sprite != null)
            {
                icon.gameObject.SetActive(true);
                icon.sprite = type.Sprite;
            }
        }



        private void SetCheckerBoardPattern()
        {
            if ((modelPosition.x % 2 == 1 && modelPosition.y % 2 == 1) ||
                (modelPosition.x % 2 == 0 && modelPosition.y % 2 == 0))
                fill.sprite = evenSprite;
            else
                fill.sprite = oddSprite;
        }
        private Vector2 ModelPosToViewPos(Vector2 modelPosition)
        {
            //строки положения нумеруются сверху вниз, поэтому Position.y отрицательный
            return new Vector2(modelPosition.x, -modelPosition.y);
        }
    }
}