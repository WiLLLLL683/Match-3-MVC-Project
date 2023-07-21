using Model.Infrastructure;
using UnityEngine;

namespace Presenter
{
    public interface IHudPresenter
    {
        public GameObject gameObject { get; }
        
        void Init(Game _game);
    }
}