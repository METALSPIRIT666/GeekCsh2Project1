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
        private static Ship ship;
        private static Timer timer = new Timer { Interval = 20 };

        const int startingObjectsNumber = 100;
        const int objectsNumber = 70;
        const int asteroidsNumber = 120;
        const int leftSpawnLine = 20;
        static int rightSpawnLine;
        static int midleLine;
        const int topSpawnLine = 10;
        static int bottomSpawnLine;
        const int maxSpeed = 3;
        const int starXspeed = 5;
        const int starYSpeed = 0;
        const int shipSwiftness = 8;

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

            ship = new Ship(new Point(leftSpawnLine, midleLine), new Point(shipSwiftness, shipSwiftness), 
                    Resource.SpaceShip.Size);
        }

        /// <summary>
        /// Создает и инициализирует объекты для экрана заставки.
        /// </summary>
        public static void LoadSplashScreen()
        {
            rightSpawnLine = Width - 20;
            bottomSpawnLine = Height - 20;
            midleLine = Height / 2;
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
            timer.Start();
            timer.Tick += TimerTick;
            Ship.MessageDie += Finish;
            form.KeyDown += FormKeyDown;
        }
        private static void TimerTick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) bullet = new Bullet(new
                Point(ship.Rect.X + 10, ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();
        }

        public static void Finish()
        {
            timer.Stop();
            buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif,
            60, FontStyle.Underline), Brushes.White, 200, 100);
            buffer.Render();
        }

        /// <summary>
        /// Выполняет отрисовку кадра.
        /// </summary>
        public static void Draw()
        {
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objArray)
            {
                obj?.Draw();
            }
            if (asteroids != null)
            {
                foreach (Asteroid a in asteroids)
                {
                    a?.Draw();
                }
            }
            bullet?.Draw();
            ship?.Draw();
            if (ship != null)
            {
                buffer.Graphics.DrawString("Energy:" + ship.Energy,
                    SystemFonts.DefaultFont, Brushes.White, 0, 0);
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
                obj?.Update();
            }
            if (asteroids != null)
            {
                for (var i = 0; i < asteroids.Length; i++)
                {
                    if (asteroids[i] == null) continue;
                    asteroids[i].Update();
                    if (bullet != null && bullet.Collision(asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        asteroids[i] = null;
                        bullet = null;
                        continue;
                    }
                    if (!ship.Collision(asteroids[i])) continue;
                    ship?.EnergyLow(asteroids[i].Power);
                    System.Media.SystemSounds.Asterisk.Play();
                    asteroids[i] = null;
                    if (ship.Energy <= 0) ship?.Die();
                }
            }
            bullet?.Update();
        }

    }
}
