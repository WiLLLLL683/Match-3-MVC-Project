namespace Presenter
{
    /// <summary>
    /// Основной интерфейс для всех презентеров
    /// Вызывается в MetaBootstrap и CoreBootstrap при загрузке/выгрузке сцены
    /// </summary>
    public interface IPresenter
    {
        public void Enable();
        public void Disable();
    }
}