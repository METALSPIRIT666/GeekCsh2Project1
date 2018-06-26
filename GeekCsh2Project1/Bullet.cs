﻿using System;
using System.Drawing;

namespace GeekCsh2Project1
{
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawRectangle(Pens.OrangeRed, pos.X,
        }

        public override void Update()
        {
            pos.X += 3;
        }

        
    }
}