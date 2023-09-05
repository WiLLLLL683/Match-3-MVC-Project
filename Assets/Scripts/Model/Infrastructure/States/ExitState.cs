using Model.Systems;
using Utils;

namespace Model.Infrastructure
{
    public class ExitState : AModelState
    {
        private StateMachine<AModelState> stateMachine;

        public ExitState(StateMachine<AModelState> _stateMachine, AllSystems _systems)
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