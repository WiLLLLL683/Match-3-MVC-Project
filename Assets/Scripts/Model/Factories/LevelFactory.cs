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

        public LevelFactory(IGameBoardFactory gameBoardFactory, IBalanceFactory balanceFactory, IPatternFactory patternFactory, IHintPatternFactory hintPatternFactory)
        {
            this.gameBoardFactory = gameBoardFactory;
            this.balanceFactory = balanceFactory;
            this.patternFactory = patternFactory;
            this.hintPatternFactory = hintPatternFactory;
        }

        public Level Create(LevelSO levelConfig)
        {
            GameBoard gameBoard = gameBoardFactory.Create(levelConfig.cellConfig);
            Balance balance = balanceFactory.Create(levelConfig.blockConfig.balance);
            Pattern[] pattern = patternFactory.Create(levelConfig.blockConfig.matchPatterns);
            HintPattern[] hintPattern = hintPatternFactory.Create(levelConfig.blockConfig.hintPatterns);

            return new Level()
            {
                gameBoard = gameBoard,
                goals = levelConfig.goals.MemberwiseArrayClone(),
                restrictions = levelConfig.restrictions.MemberwiseArrayClone(),
                balance = balance,
                matchPatterns = pattern,
                hintPatterns = hintPattern
            };
        }
    }
}