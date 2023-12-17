namespace Utils
{
    /// <summary>
    /// Дженерик стейт-машина для стейтов типа TState
    /// </summary>
    public interface IStateMachine
    {
        IExitableState CurrentState { get; }

        /// <summary>
        /// Запустить текущий стейт
        /// </summary>
        void EnterState<T>() where T : IState;

        /// <summary>
        /// Запустить текущий стейт с передачей параметра
        /// </summary>
        void EnterState<T, TPayLoad>(TPayLoad payLoad) where T : IPayLoadedState<TPayLoad>;

        /// <summary>
        /// Добавить новый стейт в стейт-машину
        /// </summary>
        void AddState(IExitableState state);

        /// <summary>
        /// Получить экземпляр стейта определенного типа
        /// </summary>
        T GetState<T>() where T : IExitableState;
    }
}