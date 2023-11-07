using Model.Objects;
using Presenter;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// Точка входа для сцены мета-игры
    /// Запускает все презентеры на сцене
    /// </summary>
    public class MetaBootstrap : MonoBehaviour
    {
        [SerializeField] private Game game; //For debug in inspector
        private IHeaderPresenter header;
        private ILevelSelectionPresenter levelSelection;

        [Inject]
        public void Construct(Game game, IHeaderPresenter header, ILevelSelectionPresenter levelSelection)
        {
            this.game = game;
            this.header = header;
            this.levelSelection = levelSelection;
        }

        private void Start()
        {
            header.Enable();
            levelSelection.Enable();
        }

        private void OnDestroy()
        {
            header?.Disable();
            levelSelection?.Disable();
        }
    }
}