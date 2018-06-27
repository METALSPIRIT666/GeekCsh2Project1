using System;
using System.Drawing;

namespace GeekCsh2Project1
{
    class MedPack : Asteroid
    {
        public MedPack(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            var rnd = new Random();
            Power = rnd.Next(-6, -2);
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(Resource.MedPack, pos.X, pos.Y);
        }

        public override void Update()
        {
            pos.X = pos.X - dir.X;
        }
    }
}
