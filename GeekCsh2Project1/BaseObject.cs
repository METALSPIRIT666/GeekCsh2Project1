using System;
using System.Drawing;

namespace GeekCsh2Project1
{
    /// <summary>
    /// Описывает базовый игровой объект: его координаты,
    ///  направление движения, размер и поведение,
    ///  а также всевозможное методы для его графической отрисовки.
    /// </summary>
    public class BaseObject
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        /// <summary>
        /// Отрисовывает игровой объект (в общем случае - астероид).
        /// </summary>
        public virtual void Draw()
        {
            //Game.buffer.Graphics.DrawEllipse(Pens.White, pos.X, pos.Y, size.Width, size.Height);
            Game.buffer.Graphics.DrawImage(Resource.Asteroid, pos.X, pos.Y);
        }

        /// <summary>
        /// Обновляет поведение объекта для следующего вызова отрисовки.
        /// </summary>
        public virtual void Update()
        {
            pos.X += dir.X;
            pos.Y += dir.Y;
            if (pos.X < 0) dir.X = -dir.X;
            if (pos.X > Game.Width - size.Width) dir.X = -dir.X;
            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y > Game.Height - size.Height) dir.Y = -dir.Y;
        }
    }
}
