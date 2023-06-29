using Model;
using Model.Objects;
using UnityEngine;
using ViewElements;

namespace Controller
{
    public interface IBlockController
    {
        public void Init();
        public void Destroy(Block block);
        public void Drag(Vector2 deltaPosition);
        public void Move(Directions direction);
        public void Activate();
    }
}