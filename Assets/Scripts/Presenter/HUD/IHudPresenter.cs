using Model.Infrastructure;
using System;
using UnityEngine;

namespace Presenter
{
    public interface IHudPresenter
    {
        public GameObject gameObject { get; }
        
        void Init(Game _game);
    }
}