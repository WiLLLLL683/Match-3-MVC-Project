using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// ������ �������� �����
    /// </summary>
    public class Block
    {
        public ABlockType Type { get; private set; }
        public Cell Cell { get; private set; }
        public Vector2Int Position => Cell.Position;
        public event BlockDelegate OnDestroy;
        public event BlockDelegate OnTypeChange;
        public event BlockDelegate OnPositionChange;

        public Block(ABlockType _type, Cell _cell)
        {
            Type = _type;
            Cell = _cell;
        }

        /// <summary>
        /// ������ ������ � ������� ���������� ����
        /// </summary>
        public void ChangePosition(Cell _cell)
        {
            Cell = _cell;
            OnPositionChange?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// �������� ��� �����
        /// </summary>
        public void ChangeType(ABlockType _type)
        {
            Type = _type;
            OnTypeChange?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// ������������ ����, ������� �� ���� �����
        /// </summary>
        public bool Activate()
        {
            return Type.Activate();
        }

        /// <summary>
        /// ���������� ����
        /// </summary>
        public void Destroy()
        {
            OnDestroy?.Invoke(this,new EventArgs());
        }
    }
}