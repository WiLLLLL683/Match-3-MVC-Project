using Data;
using Model.Factories;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IGameBoardFactory gameBoardFactory;
        private readonly IBalanceFactory balanceFactory;

        public LevelFactory(IGameBoardFactory gameBoardFactory, IBalanceFactory balanceFactory)
        {
            this.gameBoardFactory = gameBoardFactory;
            this.balanceFactory = balanceFactory;
        }

        public Level Create(LevelConfig levelData)
        {
            var gameBoard = gameBoardFactory.Create(levelData.cellConfig);
            var balance = balanceFactory.Create(levelData.blockConfig.balance);

            return new Level()
            {
                gameBoard = gameBoard,
                goals = levelData.goals.MemberwiseArrayClone(),
                restrictions = levelData.restrictions.MemberwiseArrayClone(),
                balance = balance,
                matchPatterns = levelData.blockConfig.matchPatterns.MemberwiseArrayClone(),
                hintPatterns = levelData.blockConfig.hintPatterns.MemberwiseArrayClone()
            };
        }
    }
}