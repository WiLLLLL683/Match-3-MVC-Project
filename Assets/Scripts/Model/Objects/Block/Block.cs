using System;
using UnityEngine;
namespace Model.Objects
{
    /// <summary>
    /// ������ ��� ����� �� ������� ����,
    /// ���� ������ ���������� � ������
    /// </summary>
    [Serializable]
    public class Block
    {
        public IBlockType Type;
        public Vector2Int Position;
        public bool isMarkedToDestroy;

        public Block(IBlockType type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }
    }
}