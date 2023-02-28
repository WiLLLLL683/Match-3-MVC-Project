using Model.Objects;
using System.Collections.Generic;

namespace Model.Systems
{
    public interface IMatchSystem : ISystem
    {
        public List<Cell> FindFirstHint();
        public List<Cell> FindMatches();
    }
}