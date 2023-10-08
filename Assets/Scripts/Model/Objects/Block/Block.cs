using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// ������ ��� ����� �� ������� ����,
    /// ���� ������ ���������� � ������
    /// </summary>
    [Serializable]
    public class Block : IBlock_Readonly
    {
        public BlockType Type { get; private set; }
        public Vector2Int Position { get; private set; }
        public IBlockType_Readonly Type_Readonly => Type;

        public event Action<Block> OnDestroy;
        public event Action<IBlock_Readonly> OnDestroy_Readonly;
        public event Action<IBlockType_Readonly> OnTypeChange;
        public event Action<Vector2Int> OnPositionChange;

        public Block(BlockType type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }

        /// <summary>
        /// ������ ��������� �����
        /// </summary>
        public void SetPosition(Vector2Int position)
        {
            Position = position;
            OnPositionChange?.Invoke(Position);
        }

        /// <summary>
        /// ������ ����� ��� �����
        /// </summary>
        public void SetType(BlockType type)
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
        /// ��������: ����� ������� �������� ������ �� ������
        /// </summary>
        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            OnDestroy_Readonly?.Invoke(this);
        }
    }
}