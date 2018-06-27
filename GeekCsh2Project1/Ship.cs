using System;
using System.Drawing;

namespace GeekCsh2Project1
{
    /// <summary>
    /// Описыает поведение космического корабля.
    /// </summary>
    class Ship : BaseObject
    {
        /// <summary>
        /// Запас энергии корабля, при обнулении корабль терпит крушение.
        /// </summary>
        public int Energy { get; set; } = 100;

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Метод, вычитающий энергию у корабля при столкновении.
        /// </summary>
        /// <param name="n"></param>
        public void EnergyLow(int n)
        {
            Energy -= n;   
        }

        public override void Draw()
        {
            Game.buffer.Graphics.FillEllipse(Brushes.Wheat, pos.X, pos.Y,
                size.Width, size.Height);
        }

        public override void Update()
        {         
        }

        public void Up()
        {
            if (pos.Y > 0) pos.Y = pos.Y - dir.Y;
        }

        public void Down()
        {
            if (pos.Y < Game.Height) pos.Y = pos.Y + dir.Y;
        }

        public void Die()
        {
            MessageDie?.Invoke();
        }        public static event Message MessageDie;
    }
}
