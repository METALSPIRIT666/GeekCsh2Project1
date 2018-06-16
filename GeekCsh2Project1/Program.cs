using System;
using System.Windows.Forms;

namespace GeekCsh2Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 800;
            form.Height = 600;

            Game game = new Game();
            game.Init(form);
            form.Show();
            game.Draw();
            Application.Run(form);
        }
    }
}
