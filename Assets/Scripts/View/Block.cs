using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private float moveSpeed;

        private Model.Objects.Block blockModel;
        //private Cell cell; //TODO нужно ли?
        private Vector2 targetPosition;

        public void Init(Model.Objects.Block _blockData)
        {
            blockModel = _blockData;

            ChangeType(_blockData, null);
            SetTargetPosition(_blockData, null);

            blockModel.OnDestroy += DestroyBlock;
            blockModel.OnPositionChange += SetTargetPosition;
            blockModel.OnTypeChange += ChangeType;
        }
        private void OnDestroy()
        {
            blockModel.OnDestroy -= DestroyBlock;
            blockModel.OnPositionChange -= SetTargetPosition;
            blockModel.OnTypeChange -= ChangeType;
        }
        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }



        private void SetTargetPosition(Model.Objects.Block sender, EventArgs eventArgs)
        {
            targetPosition = sender.Position;
        }
        private void ChangeType(Model.Objects.Block sender, EventArgs eventArgs)
        {
            icon.sprite = sender.Type.Sprite;
        }
        private void DestroyBlock(Model.Objects.Block sender, EventArgs eventArgs)
        {
            Destroy(gameObject);
        }
    }
}