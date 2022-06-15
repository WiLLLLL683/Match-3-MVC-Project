using Model.Objects;
using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct LevelData
    {
        public GameBoardData gameBoard;
        public CounterData[] goals;
        public CounterData[] restrictions;
        public BalanceData balance;

        public LevelData(GameBoardData _gameBoard, CounterData[] _goals, CounterData[] _restrictions, BalanceData _balance)
        {
            gameBoard = _gameBoard;
            goals = _goals;
            restrictions = _restrictions;
            balance = _balance;
        }
    }
}
