using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// ����� ����� ��������� ���� � �������� �������
    /// </summary>
    public class SpawnBlockAction : IAction
    {
        private Level level;
        ABlockType type;
        Vector2Int position;

        public SpawnBlockAction(Level _level, ABlockType _type, Vector2Int _position)
        {
            level = _level;
            type = _type;
            position = _position;
        }

        public void Execute()
        {
            SpawnBlock(type, position);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }

        private void SpawnBlock(ABlockType _type, Vector2Int _position)
        {
            if (level.gameBoard.cells[_position.x, _position.y].IsPlayable &&
                level.gameBoard.cells[_position.x, _position.y].isEmpty)
            {
                Block block = new Block(_type, _position);
                level.gameBoard.cells[_position.x, _position.y].SetBlock(block);
            }
        }
    }
}