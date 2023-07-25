using AYellowpaper;
using System;
using System.Collections;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IFactory<TModel, TView> //where TView : MonoBehaviour
    {
        public TView Create(TModel model);
        public void Clear();
    }
}