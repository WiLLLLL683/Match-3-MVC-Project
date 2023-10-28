using System;
using UnityEngine;
using Model.Readonly;

namespace Model.Objects
{
    /// <summary>
    /// ������ ��� ����� �� ������� ����,
    /// ���� ������ ���������� � ������
    /// </summary>
    [Serializable]
    public class Block
    {
        public BlockType Type;
        public Vector2Int Position;

        public Block(BlockType type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }
    }
}