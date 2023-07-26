using Model.Readonly;

namespace Presenter
{
    public interface IBlockPresenter : IPresenter
    {
        public void Move(Directions direction);
        public void Activate();
    }
}