using AYellowpaper;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class AFactory<TModel, TView, TPresenter>
        where TView : MonoBehaviour
        where TPresenter : IPresenter
    {
        protected Transform parent;
        protected TView viewPrefab;
        protected List<IPresenter> allPresenters = new();

        protected AFactory(TView viewPrefab, Transform parent = null)
        {
            this.viewPrefab = viewPrefab;
            this.parent = parent;
        }

        public abstract TView Create(TModel model, out TPresenter presenter);

        public TView Create(TModel model) => Create(model, out _);
        public void SetParent(Transform parent) => this.parent = parent;
        public void Clear()
        {
            for (int i = 0; i < allPresenters.Count; i++)
            {
                allPresenters[i].Destroy();
            }

            allPresenters.Clear();
        }
        public void ClearParent()
        {
            if (parent == null)
                return;

            for (int i = 0; i < parent.childCount; i++)
            {
                GameObject.Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}