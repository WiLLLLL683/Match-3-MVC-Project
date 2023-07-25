using Data;
using Model.Infrastructure;
using UnityEngine;

namespace Presenter
{
    /// <summary>
    /// Контроллер для окна выбора уровня
    /// </summary>
    public class LevelSelectionController : MonoBehaviour, ILevelSelectionController
    {
        private Game game;
        private Bootstrap bootstrap;
        private LevelData[] allLevels;
        private LevelData selectedLevel;

        public void Init(Game game, Bootstrap bootstrap)
        {
            this.game = game;
            this.bootstrap = bootstrap;
        }
        public void Enable()
        {

        }
        public void Disable()
        {

        }

        public void SelectPreviousLevel()
        {
            //TODO
        }
        public void SelectNextLevel()
        {
            //TODO
        }
        public void StartSelectedLevel() => bootstrap.LoadCoreGame();
    }
}