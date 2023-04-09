using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer icon;

        private Model.Objects.Cell cellModel;
        private ParticleSystem destroyEffect;
        private ParticleSystem emptyEffect;

        public void Init(Model.Objects.Cell _cellModel)
        {
            cellModel = _cellModel;

            transform.localPosition = ModelPosToViewPos(_cellModel);
            //ChangeType(_cellModel, null);

            cellModel.OnDestroy += PlayDestroyEffect;
            cellModel.OnEmpty += PlayEmptyEffect;
            cellModel.OnTypeChange += ChangeType;
        }




        private void PlayDestroyEffect(Model.Objects.Cell sender, EventArgs eventArgs)
        {
            if (sender.Type.DestroyEffect == null)
                return;

            if (destroyEffect == null)
                destroyEffect = Instantiate(cellModel.Type.DestroyEffect, transform);

            destroyEffect.Play();
        }
        private void PlayEmptyEffect(Model.Objects.Cell sender, EventArgs eventArgs)
        {
            if (sender.Type.EmptyEffect == null)
                return;

            if (emptyEffect == null)
                emptyEffect = Instantiate(cellModel.Type.EmptyEffect, transform);

            emptyEffect.Play();
        }
        private void ChangeType(Model.Objects.Cell sender, EventArgs eventArgs)
        {
            if (sender.Type.EmptyEffect == null)
                icon.enabled = false;
            else
                icon.enabled = true;

            icon.sprite = sender.Type.Sprite;
        }
        private Vector2 ModelPosToViewPos(Model.Objects.Cell cell)
        {
            //строки положения нумеруются сверху вниз, поэтому Position.y отрицательный
            return new Vector2(cell.Position.x, -cell.Position.y);
        }
    }
}