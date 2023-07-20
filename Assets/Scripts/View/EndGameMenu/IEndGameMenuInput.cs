namespace View
{
    /// <summary>
    /// Методы инпута вызываемые в кнопках Unity
    /// </summary>
    public interface IEndGameMenuInput
    {
        void Input_NextLevel();
        void Input_Quit();
        void Input_Replay();
    }
}