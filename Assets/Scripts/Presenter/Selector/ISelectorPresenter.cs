namespace Presenter
{
    public interface ISelectorPresenter : IPresenter
    {
        void SelectNext();
        void SelectPrevious();
        void StartSelected();
    }
}