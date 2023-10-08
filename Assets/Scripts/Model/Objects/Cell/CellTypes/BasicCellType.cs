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
            this.isPlayable = true;
            this.canContainBlock = true;
        }
        public BasicCellType(bool isPlayable, bool canContainBlock)
        {
            this.isPlayable = isPlayable;
            this.canContainBlock = canContainBlock;
        }

        public override void DestroyCellMaterial()
        {

        }
    }
}
