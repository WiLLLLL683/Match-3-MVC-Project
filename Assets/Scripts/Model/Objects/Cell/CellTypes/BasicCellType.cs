using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class BasicCellType : CellType
    {
        public BasicCellType()
        {
            this.IsPlayable = true;
            this.CanContainBlock = true;
        }
        public BasicCellType(bool isPlayable, bool canContainBlock)
        {
            this.IsPlayable = isPlayable;
            this.CanContainBlock = canContainBlock;
        }

        public override void DestroyCellMaterial()
        {

        }
    }
}
