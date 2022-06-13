using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class Block
    {
        public IBlockType type { get; private set; }
        private Vector2Int position;

        public Block()
        {
            //TODO загрузка параметров
        }

        public void SetPosition(Vector2Int _position)
        {
            position = _position;
        }

        public void Activate()
        {
            type.Activate();
        }

        public void Destroy()
        {
            //TODO destroyEvent
        }
    }
}