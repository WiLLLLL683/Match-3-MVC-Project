namespace View.Factories
{
    public interface IBoosterButtonFactory
    {
        IBoosterButtonView Create(int id, int amount);
    }
}