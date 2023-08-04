using System;
using System.Collections;
using UnityEngine;
using Data;

namespace View
{
    /// <summary>
    /// Визуальный элемент блока.<br/>
    /// Постоянно стремится к targetPosition.
    /// Может перетаскиваться из IInput и передавать инпут для перемещения и активации блока.
    /// Может изменять свой тип и базовое положение, проигрывать анимацию нажатия и эффект разрушения.
    /// </summary>
    public class BlockView : ABlockView, IBlockInput
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float tapSpeed;
        [SerializeField] private float tapScale;

        public Vector2Int ModelPosition => modelPosition;

        public override event Action<Directions> OnMove;
        public override event Action OnActivate;

        private ABlockType type;
        private Vector2 targetPosition;
        private Vector2Int modelPosition;
        private ParticleSystem destroyEffect;

        public override void Init(ABlockType type, Vector2Int modelPosition)
        {
            ChangeType(type);
            ChangeModelPosition(modelPosition);

            transform.localPosition = (Vector2)modelPosition.ToViewPos();
        }
        private void Update()
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        }

        //Input
        public void Input_MoveBlock(Directions direction) => OnMove?.Invoke(direction);
        public void Input_ActivateBlock() => OnActivate?.Invoke();
        public void Input_Drag(Directions direction, Vector2 deltaPosition) => targetPosition = modelPosition.ToViewPos() + deltaPosition;
        public void Input_Release() => targetPosition = modelPosition.ToViewPos();
        //View
        public override void ChangeModelPosition(Vector2Int modelPosition)
        {
            this.modelPosition = modelPosition;
            targetPosition = modelPosition.ToViewPos();
        }
        public override void ChangeType(ABlockType type)
        {
            this.type = type;
            icon.sprite = type.Sprite;
        }
        public override void PlayClickAnimation() => StartCoroutine(TapAnimation());
        public override void PlayDestroyEffect()
        {
            if (destroyEffect == null)
                destroyEffect = Instantiate(type.DestroyEffect, transform);

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