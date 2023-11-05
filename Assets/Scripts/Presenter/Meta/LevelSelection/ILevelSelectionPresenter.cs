namespace Presenter
{
    public interface ILevelSelectionPresenter: IPresenter
    {
        void SelectNext();
        void SelectPrevious();
        void StartSelected();
    }
}