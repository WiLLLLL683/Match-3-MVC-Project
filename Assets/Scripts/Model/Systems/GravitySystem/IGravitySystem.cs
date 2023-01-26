using Model.Objects;

namespace Model.Systems
{
    public interface IGravitySystem
    {
        void Execute();
        void SetLevel(Level level);
    }
}