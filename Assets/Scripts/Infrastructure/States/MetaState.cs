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
    public class MetaState : IState
    {
        private readonly ICoroutineRunner coroutineRunner;
        private const string META_SCENE_NAME = "Meta";

        private MetaDependencies meta;

        public MetaState(ICoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }

        public void OnEnter()
        {
            coroutineRunner.StartCoroutine(LoadMeta());
        }

        public void OnExit()
        {
            DisablePresenters();
        }

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

        private IEnumerator LoadMeta()
        {
            yield return SceneManager.LoadSceneAsync(META_SCENE_NAME, LoadSceneMode.Single);
            GetSceneDependencies();
            EnablePresenters();
        }

        private void GetSceneDependencies()
        {
            meta = GameObject.FindObjectOfType<MetaDependencies>();
        }
    }
}