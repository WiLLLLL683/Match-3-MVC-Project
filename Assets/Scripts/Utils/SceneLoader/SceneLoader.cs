using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneLoader : MonoBehaviour
    {
        public LoadScreenUI loadScreenUI;

        public void UnloadScene(string sceneName)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (scene.isLoaded)
                SceneManager.UnloadSceneAsync(scene);
        }

        public IEnumerator LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Additive, params string[] scenesToUnload)
        {
            yield return loadScreenUI.Show();

            for (int i = 0; i < scenesToUnload.Length; i++)
            {
                UnloadScene(scenesToUnload[i]);
            }

            //загрузка сцены
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                loadScreenUI.SetProgress(operation.progress / 0.9f);

                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }

            //активация сцены
            Scene scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);

            StartCoroutine(loadScreenUI.Hide());

            Debug.Log($"<b><color=cyan>{sceneName} is loaded</color></b>", this);
        }
    }
}