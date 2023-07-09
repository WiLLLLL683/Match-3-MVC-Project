using Model;
using Model.Objects;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IBlockPresenter
    {
        public void Init();
        public void Destroy(Block block);
        public void Drag(Vector2 deltaPosition);
        public void Move(Directions direction);
        public void Activate();
    }
}