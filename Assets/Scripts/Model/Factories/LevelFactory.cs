using Config;
using Model.Infrastructure;
using Model.Objects;

namespace Model.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IGameBoardFactory gameBoardFactory;
        private readonly IPatternFactory patternFactory;
        private readonly IHintPatternFactory hintPatternFactory;
        private readonly ICounterFactory counterFactory;

        public LevelFactory(IGameBoardFactory gameBoardFactory, IPatternFactory patternFactory, IHintPatternFactory hintPatternFactory, ICounterFactory counterFactory)
        {
            this.gameBoardFactory = gameBoardFactory;
            this.patternFactory = patternFactory;
            this.hintPatternFactory = hintPatternFactory;
            this.counterFactory = counterFactory;
        }

        public Level Create(LevelSO levelConfig)
        {
            GameBoard gameBoard = gameBoardFactory.Create(levelConfig);
            Pattern[] pattern = patternFactory.Create(levelConfig.matchPatterns);
            HintPattern[] hintPattern = hintPatternFactory.Create(levelConfig.hintPatterns);
            Counter[] goals = counterFactory.Create(levelConfig.goals);
            Counter[] restrictions = counterFactory.Create(levelConfig.restrictions);

            return new Level()
            {
                gameBoard = gameBoard,
                goals = goals,
                restrictions = restrictions,
                matchPatterns = pattern,
                hintPatterns = hintPattern
            };
        }
    }
}