using UnityEngine;
using Model.Objects;
using Model;
using View;

namespace Presenter
{
    public interface IBlockPresenter
    {
        public void Init();
        public void Destroy(Block block);
        public void Drag(Directions direction, Vector2 deltaPosition);
        public void Move(Directions direction);
        public void Activate();
    }
}