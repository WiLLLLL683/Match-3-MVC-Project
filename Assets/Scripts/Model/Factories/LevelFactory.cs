using Config;
using Model.Infrastructure;
using Model.Objects;

namespace Model.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IGameBoardFactory gameBoardFactory;
        private readonly IMatchPatternFactory patternFactory;
        private readonly ICounterFactory counterFactory;

        public LevelFactory(IGameBoardFactory gameBoardFactory, IMatchPatternFactory patternFactory, ICounterFactory counterFactory)
        {
            this.gameBoardFactory = gameBoardFactory;
            this.patternFactory = patternFactory;
            this.counterFactory = counterFactory;
        }

        public Level Create(LevelSO levelConfig)
        {
            GameBoard gameBoard = gameBoardFactory.Create(levelConfig);
            MatchPattern[] matchPatterns = patternFactory.Create(levelConfig.matchPatterns);
            Counter[] goals = counterFactory.Create(levelConfig.goals);
            Counter[] restrictions = counterFactory.Create(levelConfig.restrictions);

            return new Level()
            {
                gameBoard = gameBoard,
                goals = goals,
                restrictions = restrictions,
                matchPatterns = matchPatterns,
            };
        }
    }
}