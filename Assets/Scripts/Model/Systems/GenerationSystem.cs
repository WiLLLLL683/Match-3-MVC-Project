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
            for (int x = 0; x < level.gameBoard.cells.GetLength(0); x++)
            {
                SpawnBlockAction spawnAction = new SpawnBlockAction(level,new RedBlockType(), new Vector2Int(x,0)); //TODO вставить систему рандомного типа блока
                spawnAction.Execute();
            }
        }

        public bool SpawnBonusBlock(ABlockType _type, Vector2Int _position)
        {
            if (!level.gameBoard.cells[_position.x, _position.y].IsPlayable)
            {
                return false;
            }

            if (!level.gameBoard.cells[_position.x, _position.y].isEmpty)
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