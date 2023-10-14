using Config;
using Model.Objects;

namespace Model.Factories
{
    public class MatchPatternFactory : IMatchPatternFactory
    {
        private readonly IHintPatternFactory hintPatternFactory;

        public MatchPatternFactory(IHintPatternFactory hintPatternFactory)
        {
            this.hintPatternFactory = hintPatternFactory;
        }

        public MatchPattern Create(MatchPatternSO config)
        {
            bool[,] grid = config.GetBoolGrid();
            HintPattern[] hints = hintPatternFactory.Create(config.hintPatterns);
            return new MatchPattern(grid, hints);
        }

        public MatchPattern[] Create(MatchPatternSO[] configs)
        {
            MatchPattern[] patterns = new MatchPattern[configs.Length];

            for (int i = 0; i < patterns.Length; i++)
            {
                patterns[i] = Create(configs[i]);
            }

            return patterns;
        }
    }
}