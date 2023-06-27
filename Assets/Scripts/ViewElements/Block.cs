using Data;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ViewElements
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float tapSpeed;
        [SerializeField] private float tapScale;

        private Model.Objects.Block blockModel;
        [SerializeField] private ABlockType type;
        //private Cell cell; //TODO нужно ли?
        private Vector2 targetPosition;
        private ParticleSystem destroyEffect;

        public void Init(Model.Objects.Block _blockModel)
        {
            blockModel = _blockModel;
            type = blockModel.Type;

            transform.localPosition = ModelPosToViewPos(_blockModel);
            ChangeType(_blockModel, null);
            SetTargetPosition(_blockModel, null);

            blockModel.OnDestroy += PlayDestroyEffect;
            blockModel.OnPositionChange += SetTargetPosition;
            blockModel.OnTypeChange += ChangeType;
        }
        private void OnDestroy()
        {
            if (blockModel != null)
            {
                blockModel.OnDestroy -= PlayDestroyEffect;
                blockModel.OnPositionChange -= SetTargetPosition;
                blockModel.OnTypeChange -= ChangeType;
            }
        }
        private void Update()
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        }
        public void GrabBlock(Vector2 deltaPosition)
        {
            targetPosition = ModelPosToViewPos(blockModel) + deltaPosition;
        }
        public void ReturnBlock()
        {
            SetTargetPosition(blockModel, null);
        }
        public void TapBlock()
        {
            StartCoroutine(TapRoutine());
        }



        private IEnumerator TapRoutine()
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
        private void PlayDestroyEffect(Model.Objects.Block sender, EventArgs eventArgs)
        {
            if (destroyEffect == null)
                destroyEffect = Instantiate(blockModel.Type.DestroyEffect, transform);

            destroyEffect.Play();
            Destroy(gameObject);
        }
        private void ChangeType(Model.Objects.Block sender, EventArgs eventArgs)
        {
            icon.sprite = sender.Type.Sprite;
        }
        private void SetTargetPosition(Model.Objects.Block sender, EventArgs eventArgs) => targetPosition = ModelPosToViewPos(sender);
        private Vector2 ModelPosToViewPos(Model.Objects.Block block)
        {
            //строки положения нумеруются сверху вниз, поэтому Position.y отрицательный
            return new Vector2(block.Position.x, -block.Position.y);
        }
    }
}