using System;
using System.Windows.Forms;
using System.Drawing;

namespace GeekCsh2Project1
{
    /// <summary>
    /// Описывает инициализацию игры и все методы, необходимые для отрисовки графики на форме
    /// </summary>
    public class Game
    {
        private static BufferedGraphicsContext context;
        public static BufferedGraphics buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] objArray;

        /// <summary>
        /// Создает и инициализирует игровые объекты.
        /// </summary>
        public void Load()
        {
            const int objectsNumber = 50;
            const int leftSpawnLine = 20;
            int rightSpawnLine = Width - 20;
            const int topSpawnLine = 20;
            int bottomSpawnLine = Height - 20;
            const int maxSpeed = 3;
            const int starXspeed = 5;
            const int starYSpeed = 0;

            Random r = new Random();
            objArray = new BaseObject[objectsNumber];
            for (int i = 0; i < objArray.Length / 2; i++)
                objArray[i] = new BaseObject(new Point(r.Next(leftSpawnLine, rightSpawnLine), r.Next(topSpawnLine, bottomSpawnLine)),
                    new Point(r.Next(-maxSpeed, maxSpeed), r.Next(-maxSpeed, maxSpeed)), Resource.Asteroid.Size);
            for (int i = objArray.Length / 2; i < objArray.Length; i++)
                objArray[i] = new Star(new Point(r.Next(leftSpawnLine, rightSpawnLine), r.Next(topSpawnLine, bottomSpawnLine)),
                    new Point(starXspeed, starYSpeed), new Size(6,6));
        }

        /// <summary>
        /// Инициализирует графику для выбранной формы.
        /// </summary>
        /// <param name="form"></param>
        public void Init(Form form)
        {
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 20 };
            timer.Start();
            timer.Tick += TimerTick;
        }
        private void TimerTick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Выполняет отрисовку кадра.
        /// </summary>
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

        /// <summary>
        /// Обновляет параметры движения игровых объектов.
        /// </summary>
        public void Update()
        {
            foreach (BaseObject obj in objArray)
            {
                obj.Update();
            }
        }
    }
}
