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

        /// <summary>
        /// ������ ������ � ������� ���������� ����
        /// </summary>
        public void ChangePosition(Cell _cell)
        {
            cell = _cell;
            OnPositionChange?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// �������� ��� �����
        /// </summary>
        public void ChangeType(ABlockType _type)
        {
            type = _type;
            OnTypeChange?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// ������������ ����, ������� �� ���� �����
        /// </summary>
        public bool Activate()
        {
            return type.Activate();
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