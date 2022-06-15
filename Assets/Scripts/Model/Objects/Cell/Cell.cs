using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class Cell
    {
        public bool isPlayable { get { return type is not NotPlayableCellType; } }
        public bool isEmpty { get { return CheckEmpty(); } }
        public ACellType type { get; private set; }
        public Block block { get; private set; }

        public event CellDelegate emptyEvent;

        public Cell(ACellType _type)
        {
            type = _type;
        }

        public void SetBlock(Block _block)
        {
            if (isPlayable && _block != null)
            {
                block = _block;
            }
        }

        public void DestroyBlock()
        {
            if (isPlayable && block != null)
            {
                block.Destroy();
                block = null;
                emptyEvent?.Invoke(this, new EventArgs());
            }
        }

        private bool CheckEmpty()
        {
            if (isPlayable && block == null)
            {
                emptyEvent?.Invoke(this,new EventArgs());
                return true;
            }
            return false;
        }
    }
}
