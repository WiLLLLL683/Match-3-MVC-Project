using AYellowpaper;
using System;
using System.Collections;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class FactoryBase<TModel, TView, TPresenter> where TView : MonoBehaviour
    {
        protected Transform parent;
        protected TView viewPrefab;

        protected FactoryBase(TView viewPrefab, Transform parent = null)
        {
            this.viewPrefab = viewPrefab;
            this.parent = parent;
        }

        public abstract TView Create(TModel model, out TPresenter presenter);
        public abstract void Clear();

        public TView Create(TModel model) => Create(model, out _);
        public void SetParent(Transform parent) => this.parent = parent;
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