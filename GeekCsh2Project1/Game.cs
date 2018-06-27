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
        private static BaseObject[] objArray;
        private static Asteroid[] asteroids;
        private static Bullet bullet;

        const int startingObjectsNumber = 100;
        const int objectsNumber = 70;
        const int asteroidsNumber = 120;
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
            asteroids = new Asteroid[asteroidsNumber];

            for (int i = 0; i < asteroids.Length; i++)
                asteroids[i] = new Asteroid(new Point(r.Next(leftSpawnLine, rightSpawnLine), r.Next(topSpawnLine, bottomSpawnLine)),
                    new Point(r.Next(-maxSpeed, maxSpeed), r.Next(-maxSpeed, maxSpeed)), Resource.Asteroid.Size);

            for (int i = 0; i < objArray.Length; i++)
                objArray[i] = new Star(new Point(r.Next(leftSpawnLine, rightSpawnLine), r.Next(topSpawnLine, bottomSpawnLine)),
                    new Point(starXspeed, starYSpeed), new Size(6,6));

            bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
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
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objArray)
            {
                if (obj != null) obj.Draw();
            }
            if (asteroids != null)
            {
                foreach (Asteroid a in asteroids)
                {
                    if (a != null) a.Draw();
                }
            }
            if (bullet != null) bullet.Draw();
            buffer.Render();
        }

        /// <summary>
        /// Обновляет параметры движения игровых объектов.
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in objArray)
            {
                if (obj != null) obj.Update();
            }
            if (asteroids != null)
            {
                foreach (Asteroid a in asteroids)
                {
                    if (a != null)
                    {
                        a.Update();
                        if (a.Collision(bullet))
                        {
                            System.Media.SystemSounds.Hand.Play();
                            a.Collide(bullet);
                            bullet.Return();
                        }
                    }   
                }
            }
            if (bullet != null) bullet.Update();
        }

    }
}
