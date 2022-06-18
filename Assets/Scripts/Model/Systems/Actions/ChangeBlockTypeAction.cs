using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    public class ChangeBlockTypeAction : IAction
    {
        private Level level;
        ABlockType type;
        Vector2Int position;

        public ChangeBlockTypeAction(Level _level, ABlockType _type, Vector2Int _position)
        {
            level = _level;
            type = _type;
            position = _position;
        }

        public void Execute()
        {
            if (level == null || type == null || position == null)
            {
                Debug.LogError("Invalid input data");
                return;
            }

            ChangeBlockType(type, position);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }

        private void ChangeBlockType(ABlockType _type, Vector2Int _position)
        {
            try
            {
                if (level.gameBoard.cells[_position.x, _position.y].isPlayable &&
                    !level.gameBoard.cells[_position.x, _position.y].isEmpty)
                {
                    level.gameBoard.cells[_position.x, _position.y].block.ChangeType(_type);
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                Debug.LogError("Invalid position");
            }
        }
    }
}