using Config;
using Model.Objects;

namespace Model.Factories
{
    public class HintPatternFactory : IHintPatternFactory
    {
        public HintPattern Create(HintPatternSO config)
        {
            return new HintPattern(config.GetBoolGrid(), config.cellToMove, config.directionToMove);
        }

        public HintPattern[] Create(HintPatternSO[] configs)
        {
            HintPattern[] patterns = new HintPattern[configs.Length];

            for (int i = 0; i < patterns.Length; i++)
            {
                patterns[i] = Create(configs[i]);
            }

            return patterns;
        }
    }
}