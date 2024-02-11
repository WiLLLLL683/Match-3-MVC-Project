using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Utils;
using DG.Tweening;
using NaughtyAttributes;
using Cysharp.Threading.Tasks;
using System.Threading;
using Config;

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
        [SerializeField] private Animation clickAnimation;

        public Vector2Int ModelPosition => modelPosition;

        private BlockTypeConfig config;
        private Vector2 targetPosition;
        private Vector2Int modelPosition;
        private Vector3 positionChache;
        private Vector2 velocity;

        public void Init(Sprite iconSprite, BlockTypeConfig config, Vector2Int modelPosition)
        {
            SetType(iconSprite, config);
            SetModelPosition(modelPosition);

            transform.localPosition = (Vector2)modelPosition;
        }

        private void Start() => positionChache = transform.localPosition;

        private void Update()
        {
            velocity = (transform.localPosition - positionChache) / Time.deltaTime;
            positionChache = transform.localPosition;
            transform.localPosition = Vector2.SmoothDamp(transform.localPosition, targetPosition, ref velocity, config.smoothTime);
        }

        public void Drag(Vector2 deltaPosition) => targetPosition = modelPosition + deltaPosition;
        public void Release() => targetPosition = modelPosition;
        public async UniTask FlyTo(Vector2 localPosition, float duration, CancellationToken token = default)
        {
            targetPosition = localPosition;
            this.enabled = false;
            icon.sortingLayerName = config.flyingLayer;
            icon.maskInteraction = SpriteMaskInteraction.None;
            await UniTask.WhenAll
            (
                transform.DOLocalMove(localPosition, duration).SetEase(config.flyingEase).WithCancellation(token),
                transform.DOPunchScale(new(config.flyingScale, config.flyingScale), duration, 0, 0).SetEase(config.flyingEase).WithCancellation(token)
            );
            //TODO возврат на изначальный слой?
        }

        public void SetModelPosition(Vector2Int modelPosition)
        {
            this.modelPosition = modelPosition;
            targetPosition = modelPosition;
        }

        public void SetType(Sprite iconSprite, BlockTypeConfig typeConfig)
        {
            this.config = typeConfig;
            icon.sprite = iconSprite;
        }

        public void PlayClickAnimation() => clickAnimation.Play();

        public void Destroy()
        {
            Instantiate(config.destroyEffect, transform.position, Quaternion.identity).Play();
            GameObject.Destroy(gameObject);
        }
    }
}