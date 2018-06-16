using System;
using System.Windows.Forms;
using System.Drawing;

namespace GeekCsh2Project1
{
    public class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] objArray;
        public void Load()
        {
            Random r = new Random();
            objArray = new BaseObject[50];
            for (int i = 0; i < objArray.Length / 2; i++)
                objArray[i] = new BaseObject(new Point(r.Next(10, Width - 10), r.Next(10, Height - 10)),
                    new Point(r.Next(-10, 10), r.Next(-10, 10)), new Size(9, 9));
            for (int i = objArray.Length / 2; i < objArray.Length; i++)
                objArray[i] = new Star(new Point(r.Next(10, Width - 10), r.Next(10, Height - 10)),
                    new Point(15, 0), new Size(6,6));
        }
        public void Init(Form form)
        {
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += TimerTick;
        }
        private void TimerTick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public void Draw()
        {
            //buffer.Graphics.Clear(Color.Black);
            //buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            //buffer.Render();
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objArray)
            {
                obj.Draw();
            }
            buffer.Render();
        }
        public void Update()
        {
            foreach (BaseObject obj in objArray)
            {
                obj.Update();
            }
        }
    }
}
