using Config;
using Cysharp.Threading.Tasks;
using Model.Services;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Infrastructure
{
    /// <summary>
    /// Стейт мета-игры для включения/отключения презентеров.
    /// Переход в кор-игру происходит в презентерах.
    /// </summary>
    public class MetaState : IState
    {
        private const string META_SCENE_NAME = "Meta";
        private const string CORE_SCENE_NAME = "Core";

        private readonly SceneLoader sceneLoader;
        private MetaDependencies meta;

        public MetaState(SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        public async UniTask OnEnter(CancellationToken token)
        {
            await sceneLoader.LoadScene(META_SCENE_NAME, LoadSceneMode.Additive, CORE_SCENE_NAME);
            GetSceneDependencies();
            EnablePresenters();
        }

        public async UniTask OnExit(CancellationToken token)
        {
            DisablePresenters();
        }

        private void GetSceneDependencies() => meta = GameObject.FindObjectOfType<MetaDependencies>();

        private void EnablePresenters()
        {
            meta.header.Enable();
            meta.levelSelection.Enable();
        }

        private void DisablePresenters()
        {
            meta.header.Disable();
            meta.levelSelection.Disable();
        }
    }
}