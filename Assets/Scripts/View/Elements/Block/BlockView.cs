using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace View
{
    /// <summary>
    /// Визуальный элемент блока.<br/>
    /// Постоянно стремится к targetPosition.
    /// Может перетаскиваться из IInput и передавать инпут для перемещения и активации блока.
    /// Может изменять свой тип и базовое положение, проигрывать анимацию нажатия и эффект разрушения.
    /// </summary>
    public class BlockView : MonoBehaviour, IBlockView, IBlockInput
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float tapSpeed;
        [SerializeField] private float tapScale;

        public Vector2Int ModelPosition => modelPosition;

        public event Action<Vector2Int, Directions> OnInputMove;
        public event Action<Vector2Int> OnInputActivate;

        private Sprite iconSprite;
        private ParticleSystem destroyEffectPrefab;
        private Vector2 targetPosition;
        private Vector2Int modelPosition;
        private ParticleSystem destroyEffect;

        public void Init(Sprite iconSprite, ParticleSystem destroyEffectPrefab, Vector2Int modelPosition)
        {
            ChangeType(iconSprite, destroyEffectPrefab);
            ChangeModelPosition(modelPosition);

            transform.localPosition = (Vector2)modelPosition.ToViewPos();
        }

        private void Update()
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        }

        //Input
        public void Input_MoveBlock(Directions direction) => OnInputMove?.Invoke(modelPosition, direction);
        public void Input_ActivateBlock() => OnInputActivate?.Invoke(modelPosition);
        public void Input_Drag(Directions direction, Vector2 deltaPosition) => targetPosition = modelPosition.ToViewPos() + deltaPosition;
        public void Input_Release() => targetPosition = modelPosition.ToViewPos();

        //View
        public void ChangeModelPosition(Vector2Int modelPosition)
        {
            this.modelPosition = modelPosition;
            targetPosition = modelPosition.ToViewPos();
        }

        public void ChangeType(Sprite iconSprite, ParticleSystem destroyEffectPrefab)
        {
            this.iconSprite = iconSprite;
            this.destroyEffectPrefab = destroyEffectPrefab;
            icon.sprite = iconSprite;
        }

        public void PlayClickAnimation() => StartCoroutine(TapAnimation());

        public void PlayDestroyEffect()
        {
            if (destroyEffectPrefab == null)
                return;

            if (destroyEffect == null)
                destroyEffect = Instantiate(destroyEffectPrefab, transform);

            destroyEffect.Play();
        }

        private IEnumerator TapAnimation() //TODO заменить на обычную анимацию
        {
            Vector3 scaleBefore = icon.transform.localScale;
            float timer = tapSpeed / 2;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            icon.transform.localScale *= tapScale;
            timer = tapSpeed / 2;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            icon.transform.localScale = scaleBefore;
        }
    }
}