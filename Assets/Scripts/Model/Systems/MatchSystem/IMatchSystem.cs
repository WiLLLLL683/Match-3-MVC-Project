using Model.Objects;
using System.Collections.Generic;

namespace Model.Systems
{
    public interface IMatchSystem : ISystem
    {
        public HashSet<Cell> FindFirstHint();
        public HashSet<Cell> FindAllMatches();
    }
}