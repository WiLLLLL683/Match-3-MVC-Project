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
    [System.Serializable]
    public class Block
    {
        public ABlockType Type { get; private set; }
        public Cell Cell { get; private set; }
        public Vector2Int Position => Cell.Position;

        public event Action<Block> OnDestroy;
        public event Action<ABlockType> OnTypeChange;
        public event Action<Vector2> OnPositionChange;

        public Block(ABlockType type, Cell cell)
        {
            Type = type;
            Cell = cell;
        }

        /// <summary>
        /// ������ ������ � ������� ���������� ����
        /// </summary>
        public void ChangePosition(Cell cell)
        {
            Cell = cell;
            OnPositionChange?.Invoke(Position);
        }

        /// <summary>
        /// �������� ��� �����
        /// </summary>
        public void ChangeType(ABlockType type)
        {
            Type = type;
            OnTypeChange?.Invoke(type);
        }

        /// <summary>
        /// ������������ ����, ������� �� ���� �����
        /// </summary>
        public bool Activate() => Type.Activate();

        /// <summary>
        /// ���������� ����
        /// </summary>
        public void Destroy() => OnDestroy?.Invoke(this);
    }
}