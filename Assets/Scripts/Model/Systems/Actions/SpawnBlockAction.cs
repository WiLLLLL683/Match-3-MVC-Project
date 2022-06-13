using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    public class SpawnBlockAction : IAction
    {
        private Level level;
        IBlockType type;
        Vector2Int position;

        public SpawnBlockAction(Level _level, IBlockType _type, Vector2Int _position)
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

        private void SpawnBlock(IBlockType _type, Vector2Int _position)
        {
            if (level.gameBoard.cells[_position.x, _position.y].isPlayable &&
                level.gameBoard.cells[_position.x, _position.y].CheckEmpty())
            {
                Block block = new Block(_type, _position);
                level.gameBoard.cells[_position.x, _position.y].SetBlock(block);
            }
        }
    }
}