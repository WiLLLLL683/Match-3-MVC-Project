using Model.Objects;
using Utils;

namespace Model.Infrastructure
{
    public class BoosterState : AModelState
    {
        private Game game;
        private Level level;
        private StateMachine<AModelState> stateMachine;
        private BoosterInventory boosterInventory;

        private IBooster booster;

        public BoosterState(Game game, StateMachine<AModelState> stateMachine, BoosterInventory boosterInventory)
        {
            this.game = game;
            this.stateMachine = stateMachine;
            this.boosterInventory = boosterInventory;
        }

        public void SetInput(IBooster booster)
        {
            this.booster = booster;
        }

        public override void OnStart()
        {
            level = game.CurrentLevel;
            if (true)
            {

            }
            //TODO использовать бустер
            SucsessfullTurn();
        }

        public override void OnEnd()
        {

        }

        private void SucsessfullTurn()
        {
            //TODO засчитать ход в логгер
            //TODO обновить счетчики
            stateMachine.SetState<SpawnState>();
        }
    }
}