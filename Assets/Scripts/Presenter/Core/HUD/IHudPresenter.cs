using Model.Objects;
using View;

namespace Presenter
{
    public interface IHudPresenter: IPresenter
    {
        bool TryGetCounterView(Counter model, out ICounterView view);
    }
}