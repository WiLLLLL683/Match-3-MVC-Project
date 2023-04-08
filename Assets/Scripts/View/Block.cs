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

        public void Init(Model.Objects.Block _blockModel)
        {
            blockModel = _blockModel;

            transform.position = ModelPosToViewPos(_blockModel);
            ChangeType(_blockModel, null);
            SetTargetPosition(_blockModel, null);

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
        private void SetTargetPosition(Model.Objects.Block sender, EventArgs eventArgs) => targetPosition = ModelPosToViewPos(sender);
        private void ChangeType(Model.Objects.Block sender, EventArgs eventArgs)
        {
            icon.sprite = sender.Type.Sprite;
        }
        private Vector2 ModelPosToViewPos(Model.Objects.Block sender)
        {
            //строки положения нумеруются сверху вниз, поэтому Position.y отрицательный
            return new Vector2(sender.Position.x, -sender.Position.y);
        }
    }
}