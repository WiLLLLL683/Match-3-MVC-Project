using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    public class Block
    {
        public IBlockType type { get; private set; }
        private Vector2Int position;

        public Block(IBlockType _type, Vector2Int _position)
        {
            type = _type;
            position = _position;
            //TODO загрузка параметров
        }

        public void SetPosition(Vector2Int _position) //TODO дублирование данных о положении - может быть стоит дать ссылку на клетку?
        {
            position = _position;
        }

        public void ChangeType(IBlockType _type)
        {
            type = _type;
            //TODO event
        }

        public void Activate()
        {
            type.Activate();
        }

        public void Destroy()
        {
            //TODO destroyEvent
        }
    }
}