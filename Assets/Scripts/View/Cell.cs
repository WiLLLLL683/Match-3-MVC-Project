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

            cellModel.OnDestroy += PlayDestroyEffect;
            cellModel.OnEmpty += PlayEmptyEffect;
            cellModel.OnTypeChange += ChangeType;
        }

        private void PlayDestroyEffect(Model.Objects.Cell sender, EventArgs eventArgs)
        {
            if (destroyEffect == null)
                destroyEffect = Instantiate(cellModel.Type.DestroyEffect, transform);

            destroyEffect.Play();
        }

        private void PlayEmptyEffect(Model.Objects.Cell sender, EventArgs eventArgs)
        {
            if (emptyEffect == null)
                emptyEffect = Instantiate(cellModel.Type.EmptyEffect, transform);

            emptyEffect.Play();
        }

        private void ChangeType(Model.Objects.Cell sender, EventArgs eventArgs)
        {
            icon.sprite = sender.Type.Sprite;
        }
    }
}