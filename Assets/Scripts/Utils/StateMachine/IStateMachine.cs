namespace Utils
{
    /// <summary>
    /// Дженерик стейт-машина для стейтов типа TState
    /// </summary>
    public interface IStateMachine<TState> where TState : IState
    {
        TState CurrentState { get; }
        TState PreviousState { get; }

        /// <summary>
        /// Задать текущий стейт
        /// </summary>
        void EnterState<T>() where T : TState;

        /// <summary>
        /// Вернуться к предыдущему стейту
        /// </summary>
        void EnterPreviousState();

        /// <summary>
        /// Добавить новый стейт в стейт-машину
        /// </summary>
        void AddState(TState state);

        /// <summary>
        /// Получить экземпляр стейта определенного типа
        /// </summary>
        T GetState<T>() where T : TState;
    }
}