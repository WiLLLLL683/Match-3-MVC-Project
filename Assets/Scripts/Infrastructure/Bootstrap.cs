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
        private ILevelLoader sceneLoader;

        [Inject]
        public void Construct(ILevelLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        private void Start()
        {
            sceneLoader.LoadMetaGame();
        }
    }
}