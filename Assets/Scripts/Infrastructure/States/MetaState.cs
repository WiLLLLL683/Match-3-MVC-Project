using Config;
using Model.Services;
using System;
using System.Collections;
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

        private MetaDependencies meta;

        public IEnumerator OnEnter()
        {
            yield return SceneManager.LoadSceneAsync(META_SCENE_NAME, LoadSceneMode.Single);
            GetSceneDependencies();
            EnablePresenters();
        }

        public IEnumerator OnExit()
        {
            DisablePresenters();
            yield break;
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