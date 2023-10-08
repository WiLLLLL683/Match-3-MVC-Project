using Config;
using Model.Infrastructure;
using Model.Objects;

namespace Model.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IGameBoardFactory gameBoardFactory;
        private readonly IBalanceFactory balanceFactory;
        private readonly IPatternFactory patternFactory;
        private readonly IHintPatternFactory hintPatternFactory;
        private readonly ICounterFactory counterFactory;

        public LevelFactory(IGameBoardFactory gameBoardFactory, IBalanceFactory balanceFactory, IPatternFactory patternFactory, IHintPatternFactory hintPatternFactory, ICounterFactory counterFactory)
        {
            this.gameBoardFactory = gameBoardFactory;
            this.balanceFactory = balanceFactory;
            this.patternFactory = patternFactory;
            this.hintPatternFactory = hintPatternFactory;
            this.counterFactory = counterFactory;
        }

        public Level Create(LevelSO levelConfig)
        {
            GameBoard gameBoard = gameBoardFactory.Create(levelConfig);
            Balance balance = balanceFactory.Create(levelConfig.blockTypeSet);
            Pattern[] pattern = patternFactory.Create(levelConfig.matchPatterns);
            HintPattern[] hintPattern = hintPatternFactory.Create(levelConfig.hintPatterns);
            Counter[] goals = counterFactory.Create(levelConfig.goals);
            Counter[] restrictions = counterFactory.Create(levelConfig.restrictions);

            return new Level()
            {
                gameBoard = gameBoard,
                goals = goals,
                restrictions = restrictions,
                balance = balance,
                matchPatterns = pattern,
                hintPatterns = hintPattern
            };
        }
    }
}