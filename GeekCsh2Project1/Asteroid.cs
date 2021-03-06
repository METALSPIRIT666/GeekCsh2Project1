﻿using System;
using System.Drawing;

namespace GeekCsh2Project1
{
    class Asteroid : BaseObject
    {
        public int Power { get; set; }

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            var rnd = new Random();
            Power = rnd.Next(5,10);
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(Resource.Asteroid, pos.X, pos.Y);
        }

        public override void Update()
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
