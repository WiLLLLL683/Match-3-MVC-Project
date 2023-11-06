using Model.Objects;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    /// <summary>
    /// Точка входа для boot сцены
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
        private LevelLoader sceneLoader;

        [Inject]
        public void Construct(LevelLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        private void Start()
        {
            sceneLoader.LoadMetaGame();
        }
    }
}