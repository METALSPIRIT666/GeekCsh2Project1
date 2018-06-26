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

        const int startingObjectsNumber = 40;
        const int objectsNumber = 50;
        const int leftSpawnLine = 20;
        static int rightSpawnLine;
        const int topSpawnLine = 10;
        static int bottomSpawnLine;
        const int maxSpeed = 3;
        const int starXspeed = 5;
        const int starYSpeed = 0;

        /// <summary>
        /// Создает и инициализирует игровые объекты.
        /// </summary>
        public static void ReLoad()
        {
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
        /// Создает и инициализирует объекты для экрана заставки.
        /// </summary>
        public static void LoadSplashScreen()
        {
            rightSpawnLine = Width - 20;
            bottomSpawnLine = Height - 20;
            Random r = new Random();
            objArray = new BaseObject[startingObjectsNumber];
            for (int i = 0; i < objArray.Length; i++)
                objArray[i] = new Star(new Point(r.Next(leftSpawnLine, rightSpawnLine), r.Next(topSpawnLine, bottomSpawnLine)),
                    new Point(starXspeed, starYSpeed), new Size(6, 6));
        }

        /// <summary>
        /// Инициализирует графику для выбранной формы.
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            LoadSplashScreen();
            Timer timer = new Timer { Interval = 20 };
            timer.Start();
            timer.Tick += TimerTick;
        }
        private static void TimerTick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Выполняет отрисовку кадра.
        /// </summary>
        public static void Draw()
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
        public static void Update()
        {
            foreach (BaseObject obj in objArray)
            {
                obj.Update();
            }
        }

    }
}
