using AYellowpaper;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Utils
{
    /// <summary>
    /// Фабрика для создания вью, презентеров и связывания их с моделью
    /// </summary>
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

        /// <summary>
        /// Связать существующий объект вью с моделью
        /// Возвращает новый презентер
        /// </summary>
        public abstract TPresenter Connect(TView existingView, TModel model);

        /// <summary>
        /// Создать новый объект вью, создать презентер и соединить их с моделью
        /// Возвращает новый вью и новый презентер
        /// </summary>
        public virtual TView CreateView(TModel model, out TPresenter presenter)
        {
            var view = GameObject.Instantiate(viewPrefab, parent);
            presenter = Connect(view, model);
            return view;
        }

        /// <summary>
        /// Создать новый объект вью, создать презентер и соединить их с моделью
        /// Возвращает новый вью
        /// </summary>
        public TView CreateView(TModel model) => CreateView(model, out _);

        /// <summary>
        /// Задать родительский объект для создания в нем вью
        /// </summary>
        public void SetParent(Transform parent) => this.parent = parent;

        /// <summary>
        /// Уничтожить все созданные вью и презентеры
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < allPresenters.Count; i++)
            {
                allPresenters[i].Destroy();
            }

            allPresenters.Clear();
        }

        /// <summary>
        /// Уничтожить все объекты в родительском объекте
        /// Если родитель == null, ничего не произойдет
        /// </summary>
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