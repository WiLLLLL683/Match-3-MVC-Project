using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private float moveSpeed;

        private Model.Objects.Block blockModel;
        //private Cell cell; //TODO нужно ли?
        private Vector2 targetPosition;
        private ParticleSystem destroyEffect;

        public void Init(Model.Objects.Block _blockData)
        {
            blockModel = _blockData;

            ChangeType(_blockData, null);
            SetTargetPosition(_blockData, null);

            blockModel.OnDestroy += PlayDestroyEffect;
            blockModel.OnPositionChange += SetTargetPosition;
            blockModel.OnTypeChange += ChangeType;
        }
        private void OnDestroy()
        {
            blockModel.OnDestroy -= PlayDestroyEffect;
            blockModel.OnPositionChange -= SetTargetPosition;
            blockModel.OnTypeChange -= ChangeType;
        }
        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }



        private void PlayDestroyEffect(Model.Objects.Block sender, EventArgs eventArgs)
        {
            if (destroyEffect == null)
                destroyEffect = Instantiate(blockModel.Type.DestroyEffect, transform);

            destroyEffect.Play();
            Destroy(gameObject);
        }
        private void SetTargetPosition(Model.Objects.Block sender, EventArgs eventArgs)
        {
            targetPosition = sender.Position;
        }
        private void ChangeType(Model.Objects.Block sender, EventArgs eventArgs)
        {
            icon.sprite = sender.Type.Sprite;
        }
    }
}