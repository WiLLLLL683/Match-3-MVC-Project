using Presenter;
using UnityEngine;
using Utils;
using View;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// Зависимости со сцены кор-игры необходимые в GameStateMachine
    /// </summary>
    public class CoreDependencies : MonoBehaviour
    {
        public IStateMachine coreStateMachine;
        public IStateFactory stateFactory;
        public IGameBoardInput input;
        public IHudPresenter hud;
        public ICellsPresenter cells;
        public IBlocksPresenter blocks;
        public IBoostersPresenter boosters;
        public IPausePresenter pause;
        public IEndGamePresenter endGame;

        [Inject]
        public void Construct(IStateMachine coreStateMachine,
            IStateFactory stateFactory,
            IGameBoardInput input,
            IHudPresenter hud,
            ICellsPresenter cells,
            IBlocksPresenter blocks,
            IBoostersPresenter boosters,
            IPausePresenter pause,
            IEndGamePresenter endGame)
        {
            this.coreStateMachine = coreStateMachine;
            this.stateFactory = stateFactory;
            this.input = input;
            this.hud = hud;
            this.cells = cells;
            this.blocks = blocks;
            this.boosters = boosters;
            this.pause = pause;
            this.endGame = endGame;
        }
    }
}