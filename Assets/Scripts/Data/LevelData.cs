using Model.Objects;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LevelData
    {
        public GameBoardData gameBoard;
        public CounterData[] goals;
        public CounterData[] restrictions;
        public BalanceData balance;
        public PatternData matchPatterns;
        public PatternData hintPatterns;

        public LevelData(GameBoardData _gameBoard, CounterData[] _goals, CounterData[] _restrictions, BalanceData _balance)
        {
            gameBoard = _gameBoard;
            goals = _goals;
            restrictions = _restrictions;
            balance = _balance;
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
            if (balance.ValidCheck() == false) 
                return false;

            return true;
        }
    }
}
