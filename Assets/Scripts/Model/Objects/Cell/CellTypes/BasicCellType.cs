using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class BasicCellType : ICellType
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private ParticleSystem destroyEffect;
        [SerializeField] private ParticleSystem emptyEffect;
        [SerializeField] private bool canContainBlock;
        [SerializeField] private bool isPlayable;

        public BasicCellType()
        {
            this.isPlayable = true;
            this.canContainBlock = true;
        }
        public BasicCellType(bool isPlayable, bool canContainBlock)
        {
            this.isPlayable = isPlayable;
            this.canContainBlock = canContainBlock;
        }

        public Sprite Icon => icon;
        public ParticleSystem DestroyEffect => destroyEffect;
        public ParticleSystem EmptyEffect => emptyEffect;
        public bool CanContainBlock => canContainBlock;
        public bool IsPlayable => isPlayable;

        public void DestroyCellMaterial()
        {

        }
    }
}
