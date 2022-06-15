using System;
using UnityEngine;
using Array2DEditor;

namespace Data.ForUnity
{
    [CreateAssetMenu(fileName ="NewLevelData",menuName ="Data/LevelData")]
    public class LevelDataScriptable : ScriptableObject
    {
        public Array2DCellTypeEnum board;


        public LevelData GetLevelData()
        {
            GameBoardData boardData = new GameBoardData(board.GridSize.x, board.GridSize.y);

            for (int i = 0; i < board.GridSize.x; i++)
            {
                for (int j = 0; j < board.GridSize.y; j++)
                {
                    boardData.cellTypes[i,j] = DataFromEnum.GetCellType(board.GetCell(i,j));
                }
            }
                
            LevelData levelData = new LevelData();
            levelData.gameBoard = boardData;
            return levelData;
        } 
    }
}
