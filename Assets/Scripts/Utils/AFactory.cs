using System.Collections.Generic;
using UnityEngine;
using Presenter;

namespace Utils
{
    /// <summary>
    /// Фабрика для создания вью, презентеров и связывания их с моделью
    /// </summary>
    public abstract class AFactory<TModel, TView, TPresenter>
        where TView : MonoBehaviour
        where TPresenter : IPresenter
    {
        /// <summary>
        /// Вспомогательный объект представляющий связку из модели, вью и презентера
        /// </summary>
        public class MVPContainer
        {
            public TModel Model { get; private set; }
            public TView View { get; private set; }
            public TPresenter Presenter { get; private set; }

            public MVPContainer(TModel model, TView view, TPresenter presenter)
            {
                this.Model = model;
                this.View = view;
                this.Presenter = presenter;
            }
        }

        protected Transform parent;
        protected TView viewPrefab;
        protected List<IPresenter> allPresenters = new();

        protected AFactory(TView viewPrefab)
        {
            this.viewPrefab = viewPrefab;
        }

        /// <summary>
        /// Связать существующий объект вью с моделью
        /// Возвращает новый презентер
        /// </summary>
        public abstract TPresenter Connect(TView existingView, TModel model);

        /// <summary>
        /// Создать новый объект вью, создать презентер и соединить их с моделью
        /// Возвращает новый презентер
        /// </summary>
        public MVPContainer Create(TModel model)
        {
            var view = GameObject.Instantiate(viewPrefab, parent);
            var presenter = Connect(view, model);
            return new MVPContainer(model, view, presenter);
        }

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