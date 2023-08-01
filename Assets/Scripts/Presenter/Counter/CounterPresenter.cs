using Model.Readonly;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using View;

namespace Presenter
{
    /// <summary>
    /// Презентер для счетчиков в кор игре (количество ходов, целевых блоков и тд.)
    /// </summary>
    public class CounterPresenter : ICounterPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<ICounter_Readonly, ICounterView, ICounterPresenter>
        {
            public Factory(ICounterView viewPrefab, Transform parent = null) : base(viewPrefab)
            {
            }

            public override ICounterPresenter Connect(ICounterView existingView, ICounter_Readonly model)
            {
                var presenter = new CounterPresenter();
                presenter.Enable();
                existingView.Init(model.Target.Icon, model.Count);
                allPresenters.Add(presenter);
                return presenter;
            }
        }
        
        public void Enable() => throw new NotImplementedException();
        public void Disable() => throw new NotImplementedException();
        public void Destroy() => throw new NotImplementedException();
    }
}