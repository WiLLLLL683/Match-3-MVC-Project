using Cysharp.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// Дженерик стейт-машина для стейтов типа TState
    /// </summary>
    public interface IStateMachine
    {
        IExitableState CurrentState { get; }

        /// <summary>
        /// Запустить стейт и забыть
        /// </summary>
        void EnterState<T>() where T : IState;

        /// <summary>
        /// Запустить стейт с передачей параметра и забыть
        /// </summary>
        void EnterState<T, TPayLoad>(TPayLoad payLoad) where T : IPayLoadedState<TPayLoad>;

        /// <summary>
        /// Запустить стейт асинхронно
        /// </summary>
        UniTask EnterStateAsync<T>() where T : IState;

        /// <summary>
        /// Запустить стейт с передачей параметра асинхронно
        /// </summary>
        UniTask EnterStateAsync<T, TPayLoad>(TPayLoad payLoad) where T : IPayLoadedState<TPayLoad>;

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