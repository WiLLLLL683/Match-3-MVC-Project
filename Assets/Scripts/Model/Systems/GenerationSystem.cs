using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// —истема дл€ спавна новых блоков - вверху уровн€ при нехватке блоков ниже, бонусных блоков по команде
    /// </summary>
    public class GenerationSystem
    {
        private Level level;

        public GenerationSystem(Level _level)
        {
            level = _level;
        }

        public void SpawnTopLine()
        {
            for (int i = 0; i < level.gameBoard.cells.GetLength(0); i++)
            {
                SpawnBlockAction spawnAction = new SpawnBlockAction(level,new RedBlockType(), new Vector2Int(i,0)); //TODO вставить систему рандомного типа блока
                spawnAction.Execute();
            }
        }

        public bool SpawnBonusBlock(IBlockType _type, Vector2Int _position)
        {
            if (!level.gameBoard.cells[_position.x, _position.y].isPlayable)
            {
                return false;
            }

            if (!level.gameBoard.cells[_position.x, _position.y].CheckEmpty())
            {
                SpawnBlockAction spawnAction = new SpawnBlockAction(level, _type, _position);
                spawnAction.Execute();
            }
            else
            {
                ChangeBlockTypeAction changeTypeAction = new ChangeBlockTypeAction(level, _type, _position);
                changeTypeAction.Execute();
            }

            return true;
        }
    }
}