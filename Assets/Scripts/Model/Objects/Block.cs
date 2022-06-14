using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class Block
    {
        public ABlockType type { get; private set; }
        public Vector2Int position { get; private set; }
        public event BlockDelegate destroyEvent;

        public Block(ABlockType _type, Vector2Int _position)
        {
            type = _type;
            position = _position;
        }

        public void SetPosition(Vector2Int _position)
        {
            position = _position;
        }

        public void ChangeType(ABlockType _type)
        {
            type = _type;
            //TODO event
        }

        public void Activate()
        {
            type.Activate();
        }

        public void Destroy()
        {
            destroyEvent?.Invoke(this,new EventArgs());
        }
    }
}