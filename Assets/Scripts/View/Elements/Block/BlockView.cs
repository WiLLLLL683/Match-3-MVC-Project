using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Utils;

namespace View
{
    /// <summary>
    /// Визуальный элемент блока.<br/>
    /// Постоянно стремится к targetPosition.
    /// Может перетаскиваться из IInput и передавать инпут для перемещения и активации блока.
    /// Может изменять свой тип и базовое положение, проигрывать анимацию нажатия и эффект разрушения.
    /// </summary>
    [SelectionBase]
    public class BlockView : MonoBehaviour, IBlockView
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private float smoothTime = 0.1f;
        [SerializeField] private Animation clickAnimation;

        public Vector2Int ModelPosition => modelPosition;

        private ParticleSystem destroyEffectPrefab;
        private Vector2 targetPosition;
        private Vector2Int modelPosition;
        private Vector3 positionChache;
        private Vector2 velocity;

        public void Init(Sprite iconSprite, ParticleSystem destroyEffectPrefab, Vector2Int modelPosition)
        {
            SetType(iconSprite, destroyEffectPrefab);
            SetModelPosition(modelPosition);

            transform.localPosition = (Vector2)modelPosition;
        }

        private void Start() => positionChache = transform.localPosition;

        private void Update()
        {
            velocity = (transform.localPosition - positionChache) / Time.deltaTime;
            positionChache = transform.localPosition;
            transform.localPosition = Vector2.SmoothDamp(transform.localPosition, targetPosition, ref velocity, smoothTime);
        }

        public void Drag(Vector2 deltaPosition) => targetPosition = modelPosition + deltaPosition;
        public void Release() => targetPosition = modelPosition;

        public void SetModelPosition(Vector2Int modelPosition)
        {
            this.modelPosition = modelPosition;
            targetPosition = modelPosition;
        }

        public void SetType(Sprite iconSprite, ParticleSystem destroyEffectPrefab)
        {
            this.destroyEffectPrefab = destroyEffectPrefab;
            icon.sprite = iconSprite;
        }

        public void PlayClickAnimation() => clickAnimation.Play();

        public void Destroy()
        {
            if (destroyEffectPrefab == null)
                return;

            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity).Play();
            GameObject.Destroy(gameObject);
        }
    }
}