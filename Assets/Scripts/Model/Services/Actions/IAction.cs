namespace Model.Services
{
    public interface IAction
    {
        public void Execute();
        public void Undo();
    }
}