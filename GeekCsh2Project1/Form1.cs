using System;

using System.Windows.Forms;

namespace GeekCsh2Project1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Game.ReLoad();
            startButton.Visible = false;
        }

        private void startButton_Paint(object sender, PaintEventArgs e)
        {
            startButton.Top = (Height - startButton.Height) / 2;
            startButton.Left = (Width - startButton.Width) / 2;
        }
    }
}
