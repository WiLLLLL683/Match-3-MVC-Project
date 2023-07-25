using Model.Readonly;

namespace Presenter
{
    public interface IBlockPresenter : IPresenter
    {
        public void Destroy(IBlock_Readonly block);
        public void Move(Directions direction);
        public void Activate();
    }
}