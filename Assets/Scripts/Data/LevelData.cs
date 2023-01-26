using Array2DEditor;
using Model.Objects;
using System;
using UnityEngine;

namespace Data
{
    public class LevelData
    {
        public GameBoardData gameBoard;
        public CounterData[] goals;
        public CounterData[] restrictions;
        public Balance balance;
        public Pattern[] matchPatterns;
        public Pattern[] hintPatterns;

        public LevelData(GameBoardData _gameBoard, CounterData[] _goals, CounterData[] _restrictions, Balance _balance, Pattern[] _matchPatterns, Pattern[] _hintPatterns)
        {
            gameBoard = _gameBoard;
            goals = _goals;
            restrictions = _restrictions;
            balance = _balance;
            matchPatterns = _matchPatterns;
            hintPatterns = _hintPatterns;
        }

        public bool ValidCheck()
        {
            if (gameBoard.ValidCheck() == false) 
                return false;
            for (int i = 0; i < goals.Length; i++)
            {
                if (goals[i].ValidCheck() == false) 
                    return false;
            }
            for (int i = 0; i < restrictions.Length; i++)
            {
                if (restrictions[i].ValidCheck() == false) 
                    return false;
            }
            if (balance == null) 
                return false;

            return true;
        }
    }
}
