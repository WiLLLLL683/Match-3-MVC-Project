using Model.Objects;
using Model.Services;
using Utils;

namespace Model.Infrastructure
{
    public class BoosterState : AModelState
    {
        private Game game;
        private Level level;
        private IStateMachine<AModelState> stateMachine;
        private IBoosterService boosterInventory;

        private IBooster booster;

        public BoosterState(Game game, IStateMachine<AModelState> stateMachine, IBoosterService boosterInventory)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.boosterInventory = boosterInventory;
        }

        public void SetInput(IBooster booster)
        {
            this.booster = booster;
        }

        public override void OnEnter()
        {
            level = game.CurrentLevel;
            if (true)
            {

            }
            //TODO использовать бустер
            SucsessfullTurn();
        }

        public override void OnExit()
        {

        }

        private void SucsessfullTurn()
        {
            //TODO засчитать ход в логгер
            //TODO обновить счетчики
            stateMachine.EnterState<SpawnState>();
        }
    }
}