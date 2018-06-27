using System;
using System.Drawing;

namespace GeekCsh2Project1
{
    /// <summary>
    /// Описывает поведение снаряда.
    /// </summary>
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawRectangle(Pens.OrangeRed, pos.X,
                pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
            pos.X += 3;
        }

        public void Return()
        {
            pos.X = 0;
        }
    }
}
