using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Objects
{
    public abstract class ABlockType : ICounterTarget
    {
        public string testString;//TODO убрать строку в тестах

        /// <summary>
        /// Возвращает успешен ли был ход
        /// </summary>
        public virtual bool Activate()
        {
            testString = GetType().ToString();
            return false;
        }
    }
}
