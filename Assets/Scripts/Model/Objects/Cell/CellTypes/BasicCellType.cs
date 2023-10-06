using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class BasicCellType : ICellType, ICounterTarget
    {
        [SerializeField] private int id;
        [SerializeField] private bool canContainBlock;
        [SerializeField] private bool isPlayable;
        public int Id => id;
        public bool CanContainBlock => canContainBlock;
        public bool IsPlayable => isPlayable;

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

        public void DestroyCellMaterial()
        {

        }
    }
}
