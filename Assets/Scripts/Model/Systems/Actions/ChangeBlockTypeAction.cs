using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    public class ChangeBlockTypeAction : IAction
    {
        private Level level;
        IBlockType type;
        Vector2Int position;

        public ChangeBlockTypeAction(Level _level, IBlockType _type, Vector2Int _position)
        {
            level = _level;
            type = _type;
            position = _position;
        }

        public void Execute()
        {
            ChangeBlockType(type, position);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }

        private void ChangeBlockType(IBlockType _type, Vector2Int _position)
        {
            if (level.gameBoard.cells[_position.x, _position.y].isPlayable &&
                !level.gameBoard.cells[_position.x, _position.y].CheckEmpty())
            {
                level.gameBoard.cells[_position.x, _position.y].block.ChangeType(_type);
            }
        }
    }
}