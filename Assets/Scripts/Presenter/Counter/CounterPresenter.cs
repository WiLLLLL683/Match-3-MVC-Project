using System;
using UnityEngine;
using Utils;
using View;
using Model.Objects;
using Zenject;

namespace Presenter
{
    /// <summary>
    /// Презентер для счетчиков в кор игре (количество ходов, целевых блоков и тд.)
    /// </summary>
    public class CounterPresenter : ICounterPresenter
    {
        ///// <summary>
        ///// Реализация фабрики использующая класс презентера в котором находится.
        ///// </summary>
        //public class Factory : AFactory<Counter, ACounterView, ICounterPresenter>
        //{
        //    public Factory(ACounterView viewPrefab, Transform parent = null) : base(viewPrefab)
        //    {
        //    }

        //    public override ICounterPresenter Connect(ACounterView existingView, Counter model)
        //    {
        //        var presenter = new CounterPresenter();
        //        presenter.Enable();
        //        //existingView.Init(model.Target.Icon, model.Count); //TODO проброс CounterSO
        //        allPresenters.Add(presenter);
        //        return presenter;
        //    }
        //}

        public class Factory : PlaceholderFactory<CounterPresenter> { }

        public void Enable() => throw new NotImplementedException();
        public void Disable() => throw new NotImplementedException();
        public void Destroy() => throw new NotImplementedException();
    }
}