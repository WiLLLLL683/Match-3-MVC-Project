using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private SpriteRenderer fill;
        [SerializeField] private GameObject background;
        [SerializeField] private Sprite evenSprite;
        [SerializeField] private Sprite oddSprite;

        private Model.Objects.Cell cellModel;
        private ParticleSystem destroyEffect;
        private ParticleSystem emptyEffect;

        public void Init(Model.Objects.Cell _cellModel)
        {
            cellModel = _cellModel;

            transform.localPosition = ModelPosToViewPos(_cellModel);
            SetCheckerBoardPattern();
            //SetType(_cellModel, null);

            cellModel.OnDestroy += PlayDestroyEffect;
            cellModel.OnEmpty += PlayEmptyEffect;
            cellModel.OnTypeChange += SetType;
        }
        private void OnDestroy()
        {
            if (cellModel == null)
                return;

            cellModel.OnDestroy -= PlayDestroyEffect;
            cellModel.OnEmpty -= PlayEmptyEffect;
            cellModel.OnTypeChange -= SetType;
        }



        private void SetCheckerBoardPattern()
        {
            if ((cellModel.Position.x % 2 == 1 && cellModel.Position.y % 2 == 1) ||
                (cellModel.Position.x % 2 == 0 && cellModel.Position.y % 2 == 0))
                fill.sprite = evenSprite;
            else
                fill.sprite = oddSprite;
        }
        private void PlayDestroyEffect(Model.Objects.Cell sender, EventArgs eventArgs)
        {
            if (sender.Type.DestroyEffect == null)
                return;

            if (destroyEffect == null)
                destroyEffect = Instantiate(cellModel.Type.DestroyEffect, transform);

            destroyEffect.Play();
        }
        private void PlayEmptyEffect(Model.Objects.Cell cell, EventArgs eventArgs)
        {
            if (cell.Type.EmptyEffect == null)
                return;

            if (emptyEffect == null)
                emptyEffect = Instantiate(cellModel.Type.EmptyEffect, transform);

            emptyEffect.Play();
        }
        private void SetType(Model.Objects.Cell cell, EventArgs eventArgs)
        {
            if (!cell.Type.IsPlayable)
            {
                gameObject.SetActive(false);
                return;
            }

            if(cell.Type.Sprite != null)
            {
                icon.gameObject.SetActive(true);
                icon.sprite = cell.Type.Sprite;
            }
        }
        private Vector2 ModelPosToViewPos(Model.Objects.Cell cell)
        {
            //строки положения нумеруются сверху вниз, поэтому Position.y отрицательный
            return new Vector2(cell.Position.x, -cell.Position.y);
        }
    }
}