using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.GameLogic
{
    public abstract class ACoreGameState : AState
    {
        protected Game context;
        public ACoreGameState(GameStateMachine _stateMachine, Game _context) : base(_stateMachine)
        {
            context = _context;
        }
    }
}
