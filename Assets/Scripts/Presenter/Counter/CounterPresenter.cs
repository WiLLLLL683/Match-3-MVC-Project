using System;
using UnityEngine;
using Model.Readonly;
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
        public class Factory : AFactory<ICounter_Readonly, ACounterView, ICounterPresenter>
        {
            public Factory(ACounterView viewPrefab, Transform parent = null) : base(viewPrefab)
            {
            }

            public override ICounterPresenter Connect(ACounterView existingView, ICounter_Readonly model)
            {
                var presenter = new CounterPresenter();
                presenter.Enable();
                //existingView.Init(model.Target.Icon, model.Count); //TODO проброс CounterSO
                allPresenters.Add(presenter);
                return presenter;
            }
        }
        
        public void Enable() => throw new NotImplementedException();
        public void Disable() => throw new NotImplementedException();
        public void Destroy() => throw new NotImplementedException();
    }
}