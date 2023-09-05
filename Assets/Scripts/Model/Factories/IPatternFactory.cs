using Data;
using Model.Objects;

public interface IPatternFactory
{
    Pattern Create(PatternSO config);
    Pattern[] Create(PatternSO[] configs);
}