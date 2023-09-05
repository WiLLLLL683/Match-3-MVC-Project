using Config;
using Model.Objects;

namespace Model.Factories
{
    public interface IPatternFactory
    {
        Pattern Create(PatternSO config);
        Pattern[] Create(PatternSO[] configs);
    }
}