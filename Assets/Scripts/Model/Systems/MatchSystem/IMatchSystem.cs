using Model.Objects;
using System.Collections.Generic;

namespace Model.Systems
{
    public interface IMatchSystem
    {
        public List<Cell> FindFirstHint();
        public List<Cell> FindMatches();
        public void SetLevel(Level level);
    }
}