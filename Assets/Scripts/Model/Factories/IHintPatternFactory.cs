using Config;
using Model.Objects;

namespace Model.Infrastructure
{
    public interface IHintPatternFactory
    {
        HintPattern Create(HintPatternSO config);
        HintPattern[] Create(HintPatternSO[] configs);
    }
}