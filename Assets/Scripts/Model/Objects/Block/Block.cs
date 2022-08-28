using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Объект игрового блока
    /// </summary>
    public class Block
    {
        public ABlockType type { get; private set; }
        public Cell cell { get; private set; }
        public Vector2Int Position { get { return cell.position; } }
        public event BlockDelegate OnDestroy;
        public event BlockDelegate OnTypeChange;
        public event BlockDelegate OnPositionChange;

        public Block(ABlockType _type, Cell _cell)
        {
            type = _type;
            cell = _cell;
        }

        public void ChangePosition(Cell _cell)
        {
            cell = _cell;
            OnPositionChange?.Invoke(this, new EventArgs());
        }

        public void ChangeType(ABlockType _type)
        {
            type = _type;
            OnTypeChange?.Invoke(this, new EventArgs());
        }

        public bool Activate()
        {
            return type.Activate();
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this,new EventArgs());
        }
    }
}