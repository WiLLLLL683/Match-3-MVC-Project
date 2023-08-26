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
        private readonly GameBoardFactory gameBoardFactory;

        public LevelFactory(GameBoardFactory gameBoardFactory)
        {
            this.gameBoardFactory = gameBoardFactory;
        }

        public Level Create(LevelConfig levelData)
        {
            var gameBoard = gameBoardFactory.Create(levelData.cellConfig);

            return new Level()
            {
                gameBoard = gameBoard,
                goals = levelData.goals.MemberwiseArrayClone(),
                restrictions = levelData.restrictions.MemberwiseArrayClone(),
                balance = (Balance)levelData.blockConfig.balance.Clone(),
                matchPatterns = levelData.blockConfig.matchPatterns.MemberwiseArrayClone(),
                hintPatterns = levelData.blockConfig.hintPatterns.MemberwiseArrayClone()
            };
        }
    }
}