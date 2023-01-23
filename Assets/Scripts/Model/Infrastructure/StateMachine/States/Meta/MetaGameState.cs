﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model.Infrastructure
{
    public class MetaGameState : IState
    {
        private const string META_SCENE_NAME = "Meta";

        public void OnStart()
        {
            SceneManager.LoadScene(META_SCENE_NAME);
        }

        public void OnEnd()
        {

        }
    }
}