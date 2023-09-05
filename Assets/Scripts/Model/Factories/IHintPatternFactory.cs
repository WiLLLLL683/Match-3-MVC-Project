using Data;
using Model.Objects;

public interface IHintPatternFactory
{
    HintPattern Create(HintPatternSO config);
    HintPattern[] Create(HintPatternSO[] configs);
}