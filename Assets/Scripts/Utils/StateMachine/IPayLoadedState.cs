
namespace Utils
{
    public interface IPayLoadedState<TPayLoad> : IState
    {
        public void OnEnter(TPayLoad payLoad);
    }
}