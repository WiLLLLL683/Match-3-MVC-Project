using Presenter;
using UnityEngine;
using Utils;
using View;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// ����������� �� ����� ���-���� ����������� � GameStateMachine
    /// </summary>
    public class CoreDependencies : MonoBehaviour
    {
        public IStateMachine coreStateMachine;
        public IStateFactory stateFactory;
        public IInput input;
        public IHudPresenter hud;
        public ICellsPresenter cells;
        public IBlocksPresenter blocks;
        public IBoosterInventoryPresenter boosterInventory;
        public IPausePresenter pause;
        public IEndGamePresenter endGame;

        [Inject]
        public void Construct(IStateMachine coreStateMachine,
            IStateFactory stateFactory,
            IInput input,
            IHudPresenter hud,
            ICellsPresenter cells,
            IBlocksPresenter blocks,
            IBoosterInventoryPresenter boosterInventory,
            IPausePresenter pause,
            IEndGamePresenter endGame)
        {
            this.coreStateMachine = coreStateMachine;
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