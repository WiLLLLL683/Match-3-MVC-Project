using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class Cell
    {
        public bool isPlayable { get; private set; }
        public ICellType type { get; private set; }
        public Block block { get; private set; }

        public Cell()
        {
            //TODO загрузка начального состояния
        }

        public void SetBlock(Block _block)
        {
            block = _block;
        }

        public bool CheckEmpty()
        {
            if (isPlayable && block == null)
            {
                //TODO EmptyCell event
                return true;
            }
            return false;
        }
    }
}
