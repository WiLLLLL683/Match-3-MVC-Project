using Config;
using log4net.Core;
using Model.Infrastructure;
using Model.Services;
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
        public IStateMachine stateMachine;
        public IStateFactory stateFactory;
        public IInput input;
        public IHudPresenter hud;
        public ICellsPresenter cells;
        public IBlocksPresenter blocks;
        public IBoosterInventoryPresenter boosterInventory;
        public IPausePresenter pause;
        public IEndGamePresenter endGame;

        [Inject]
        public void Construct(IStateMachine stateMachine,
            IStateFactory stateFactory,
            IInput input,
            IHudPresenter hud,
            ICellsPresenter cells,
            IBlocksPresenter blocks,
            IBoosterInventoryPresenter boosterInventory,
            IPausePresenter pause,
            IEndGamePresenter endGame)
        {
            this.stateMachine = stateMachine;
            this.stateFactory = stateFactory;
            this.input = input;
            this.hud = hud;
            this.cells = cells;
            this.blocks = blocks;
            this.boosterInventory = boosterInventory;
            this.pause = pause;
            this.endGame = endGame;
        }
    }
}