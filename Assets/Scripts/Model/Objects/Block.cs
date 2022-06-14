using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class Block
    {
        public IBlockType type { get; private set; }
        public Vector2Int position { get; private set; }

        public Block(IBlockType _type, Vector2Int _position)
        {
            type = _type;
            position = _position;
            //TODO �������� ����������
        }

        public void SetPosition(Vector2Int _position) //TODO ������������ ������ � ��������� - ����� ���� ����� ���� ������ �� ������?
        {
            position = _position;
        }

        public void ChangeType(IBlockType _type)
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
            //TODO destroyEvent
        }
    }
}