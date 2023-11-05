using Model.Infrastructure;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// Точка входа для boot сцены
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
        [NaughtyAttributes.ShowNativeProperty()] public string gameModelState => game?.CurrentStateName;

        [SerializeField] private Game game;
        private LevelLoader sceneLoader;

        [Inject]
        public void Construct(Game game, LevelLoader sceneLoader)
        {
            this.game = game;
            this.sceneLoader = sceneLoader;
        }

        private void Start()
        {
            game.Init();
            sceneLoader.LoadMetaGame();
        }
    }
}