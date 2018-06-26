using System;
using System.Drawing;

namespace GeekCsh2Project1
{
    /// <summary>
    /// Описывает базовый игровой объект: его координаты,
    ///  направление движения, размер и поведение,
    ///  а также всевозможное методы для его графической отрисовки.
    /// </summary>
    public abstract class BaseObject
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        protected BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        /// <summary>
        /// Отрисовывает игровой объект.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Обновляет поведение объекта для следующего вызова отрисовки.
        /// </summary>
        public abstract void Update();
    }
}
