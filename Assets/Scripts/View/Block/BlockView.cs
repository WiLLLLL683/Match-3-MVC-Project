using System;
using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using Data;

namespace View
{
    public class BlockView : MonoBehaviour, IBlockView, IBlockInput
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float tapSpeed;
        [SerializeField] private float tapScale;

        public event Action<Directions> OnMove;
        public event Action OnActivate;
        public event Action<Directions, Vector2> OnDrag;

        private ABlockType type;
        private Vector2 targetPosition;
        private Vector2 modelPosition;
        private ParticleSystem destroyEffect;

        public void Init(ABlockType type, Vector2 modelPosition)
        {
            SetType(type);
            SetModelPosition(modelPosition);

            transform.localPosition = ModelPosToViewPos(modelPosition);
        }
        private void Update()
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        }

        public void SetModelPosition(Vector2 modelPosition)
        {
            this.modelPosition = modelPosition;
            targetPosition = ModelPosToViewPos(modelPosition);
        }
        public void SetType(ABlockType type)
        {
            this.type = type;
            icon.sprite = type.Sprite;
        }
        //Input
        public void Input_MoveBlock(Directions direction)
        {
            Debug.Log(this + " moved " + direction);
            OnMove?.Invoke(direction);
        }
        public void Input_ActivateBlock()
        {
            Debug.Log(this + " activated");
            OnActivate?.Invoke();
        }
        public void Input_Drag(Directions direction, Vector2 deltaPosition)
        {
            Debug.Log(this + " grabbed");
            OnDrag?.Invoke(direction, deltaPosition);
        }
        //View
        public void DragPosition(Vector2 deltaPosition) => targetPosition = ModelPosToViewPos(modelPosition) + deltaPosition;
        public void ReturnToModelPosition() => targetPosition = ModelPosToViewPos(modelPosition);
        public void PlayClickAnimation() => StartCoroutine(TapAnimation());
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