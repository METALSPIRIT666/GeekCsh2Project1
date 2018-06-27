using System;
using System.Windows.Forms;

namespace GeekCsh2Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            Form1 form = new Form1()
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };

            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
