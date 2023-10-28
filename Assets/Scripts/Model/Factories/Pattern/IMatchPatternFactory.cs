using Config;
using Model.Objects;

namespace Model.Factories
{
    public interface IMatchPatternFactory
    {
        MatchPattern Create(MatchPatternSO config);
        MatchPattern[] Create(MatchPatternSO[] configs);
    }
}