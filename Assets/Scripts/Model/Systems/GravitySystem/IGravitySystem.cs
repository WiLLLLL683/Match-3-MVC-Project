using Model.Objects;

namespace Model.Systems
{
    public interface IGravitySystem : ISystem
    {
        void Execute();
    }
}