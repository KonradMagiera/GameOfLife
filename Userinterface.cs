using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CellularAutomata
{
    public partial class Pane : Form
    {

        private const int tileNumber = 20;
        private const int paneDimensionSize = 850;
        private const int tileSize = (int)(paneDimensionSize / tileNumber);
        private CellularAutomata cellularAutomata;
        private Thread automata;

        public Pane()
        {
            InitializeComponent();
            cellularAutomata = new CellularAutomata(tileNumber);
            ThreadStart childref = new ThreadStart(RunAutomata);
            automata = new Thread(childref);
            automata.IsBackground = true;

        }

        private void FormMouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / tileSize;
            int y = e.Y / tileSize;
            if (x < tileNumber && y < tileNumber && x >= 0 && y >= 0)
            {
                SolidBrush myBrush = new SolidBrush(Color.Red);
                Graphics formGraphics;
                formGraphics = this.CreateGraphics();
                if (cellularAutomata.GetBoard(x, y) == 0)
                {
                    myBrush.Color = Color.Black;
                }
                else
                {
                    myBrush.Color = Color.White;
                }
                cellularAutomata.ChangeType(x, y);

                formGraphics.FillRectangle(myBrush, new Rectangle(tileSize * x, tileSize * y, tileSize, tileSize));
                myBrush.Dispose();
                formGraphics.Dispose();
            }
        }
        private void RunStopAutomata(object sender, EventArgs e)
        {
            if (StartStop.Text == "Start")
            {
                StartStop.Text = "Stop";
                try
                {
                    automata.Start();
                }
                catch
                {
#pragma warning disable CS0618 // Typ lub składowa jest przestarzała
                    automata.Resume();
#pragma warning restore CS0618 // Typ lub składowa jest przestarzała
                }
            }
            else
            {
                StartStop.Text = "Start";
#pragma warning disable CS0618 // Typ lub składowa jest przestarzała
                automata.Suspend();
#pragma warning restore CS0618 // Typ lub składowa jest przestarzała
            }

        }


        private void PanePaint(object sender, PaintEventArgs e)
        {
            SolidBrush myBrush = new SolidBrush(Color.Red);
            Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            for (int x = 0; x < tileNumber; x++)
            {
                for (int y = 0; y < tileNumber; y++)
                {
                    if(cellularAutomata.GetBoard(x, y) == 0)
                    {
                        myBrush.Color = Color.White;
                    }
                    else
                    {
                        myBrush.Color = Color.Black;
                    }
                    formGraphics.FillRectangle(myBrush, new Rectangle(tileSize * x, tileSize * y, tileSize, tileSize));
                }
            }
            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void RunAutomata()
        {
            while (true)
            {
                cellularAutomata.GenerateNextBoard();
                Invoke((MethodInvoker)delegate {
                    Refresh();
                });
                Thread.Sleep(150);
            }
        }
    }
}
