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
        public event BlockDelegate OnDestroy;
        public event BlockDelegate OnTypeChange;
        public event BlockDelegate OnPositionChange;

        public Block(ABlockType _type, Vector2Int _position)
        {
            type = _type;
            position = _position;
        }

        public void SetPosition(Vector2Int _position)
        {
            position = _position;
            OnPositionChange?.Invoke(this, new EventArgs());
        }

        public void ChangeType(ABlockType _type)
        {
            type = _type;
            OnTypeChange?.Invoke(this, new EventArgs());
        }

        public void Activate()
        {
            type.Activate();
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this,new EventArgs());
        }
    }
}