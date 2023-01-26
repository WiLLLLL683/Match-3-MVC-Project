using Array2DEditor;
using Model.Objects;
using System;
using UnityEngine;

namespace Data
{
    public class LevelData
    {
        public GameBoardData gameBoard;
        public Counter[] goals;
        public Counter[] restrictions;
        public Balance balance;
        public Pattern[] matchPatterns;
        public Pattern[] hintPatterns;

        public LevelData(GameBoardData _gameBoard, Counter[] _goals, Counter[] _restrictions, Balance _balance, Pattern[] _matchPatterns, Pattern[] _hintPatterns)
        {
            gameBoard = _gameBoard;
            goals = _goals;
            restrictions = _restrictions;
            balance = _balance;
            matchPatterns = _matchPatterns;
            hintPatterns = _hintPatterns;
        }
    }
}