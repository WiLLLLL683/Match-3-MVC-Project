using Data;
using Model.Objects;

namespace Model.Factories
{
    public class PatternFactory : IPatternFactory
    {
        public Pattern Create(PatternSO config)
        {
            return new Pattern(config.GetBoolGrid());
        }

        public Pattern[] Create(PatternSO[] configs)
        {
            Pattern[] patterns = new Pattern[configs.Length];

            for (int i = 0; i < patterns.Length; i++)
            {
                patterns[i] = Create(configs[i]);
            }

            return patterns;
        }
    }
}