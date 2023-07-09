using Presenter;
using Data;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;

namespace View
{
    public class BlockView : MonoBehaviour, IBlockView
    {
        public IBlockPresenter Controller { get; private set; }

        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float tapSpeed;
        [SerializeField] private float tapScale;

        [SerializeField] private ABlockType type;

        private Vector2 targetPosition;
        private Vector2 modelPosition;
        private ParticleSystem destroyEffect;

        public void Init(ABlockType type, Vector2 modelPosition, IBlockPresenter controller)
        {
            this.type = type;
            this.Controller = controller;

            SetModelPosition(modelPosition);
            transform.localPosition = ModelPosToViewPos(modelPosition);
            ChangeType(type);
        }
        private void Update()
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        }

        public void PlayClickAnimation() => StartCoroutine(TapAnimation());
        public void Drag(Vector2 deltaPosition) => targetPosition = ModelPosToViewPos(modelPosition) + deltaPosition;
        public void ReturnToModelPosition() => targetPosition = ModelPosToViewPos(modelPosition);
        public void ChangeType(ABlockType blockType) => icon.sprite = blockType.Sprite;
        public void SetModelPosition(Vector2 modelPosition)
        {
            this.modelPosition = modelPosition;
            targetPosition = ModelPosToViewPos(modelPosition);
        }
        public void PlayDestroyEffect()
        {
            if (destroyEffect == null)
                destroyEffect = Instantiate(type.DestroyEffect, transform);

            destroyEffect.Play();
        }

        private IEnumerator TapAnimation()
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
        private Vector2 ModelPosToViewPos(Vector2 modelPosition)
        {
            //строки положения нумеруются сверху вниз, поэтому Position.y отрицательный
            return new Vector2(modelPosition.x, -modelPosition.y);
        }
    }
}