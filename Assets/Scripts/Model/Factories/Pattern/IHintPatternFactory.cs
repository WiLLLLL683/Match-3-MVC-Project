using Config;
using Model.Objects;

namespace Model.Factories
{
    public interface IHintPatternFactory
    {
        HintPattern Create(HintPatternSO config);
        HintPattern[] Create(HintPatternSO[] configs);
    }
}