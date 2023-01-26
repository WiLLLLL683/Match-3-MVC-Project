using Model.Objects;
using System.Collections.Generic;

namespace Model.Systems
{
    public interface IMatchSystem
    {
        List<Cell> FindFirstHint();
        List<Cell> FindMatches();
        void SetLevel(Level level);
    }
}