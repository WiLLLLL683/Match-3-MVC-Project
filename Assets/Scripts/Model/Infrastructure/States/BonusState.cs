using Model.Systems;
using Utils;

namespace Model.Infrastructure
{
    public class BonusState : AModelState
    {
        private StateMachine<AModelState> stateMachine;

        public BonusState(StateMachine<AModelState> _stateMachine, AllSystems _systems)
        {
            stateMachine = _stateMachine;
        }

        public override void OnStart()
        {

        }

        public override void OnEnd()
        {

        }
    }
}