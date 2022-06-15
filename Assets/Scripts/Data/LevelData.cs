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
    }
}
