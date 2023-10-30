using UnityEngine;
using View;
using Zenject;

namespace CompositionRoot
{
    public class CoreSceneInstaller : MonoInstaller
    {
        [SerializeField] private HudView hudView;
        [SerializeField] private GameBoardView gameBoardView;
        [SerializeField] private BoosterInventoryView boosterInventoryView;
        [SerializeField] private PauseView pauseView;
        [SerializeField] private EndGameView endGameView;

        public override void InstallBindings()
        {
        }
    }
}