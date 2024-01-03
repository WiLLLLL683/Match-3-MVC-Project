namespace Model.Objects
{
    public interface IBooster
    {
        public int Id { get; }
        public void Execute();
        public IBooster Clone();
    }
}