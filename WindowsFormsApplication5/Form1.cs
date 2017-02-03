using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public class CircluarButton : UserControl
    {
        Pen myPen;
        protected override void OnPaint(PaintEventArgs pevent)
        {
            int radius = 20;
            pevent.Graphics.Clear(Color.White);
            Graphics graphics = pevent.Graphics;
            myPen = new Pen(new SolidBrush(this.BackColor), 2f);
            pevent.Graphics.FillEllipse(new SolidBrush(this.BackColor), 20 - radius, 20 - radius, radius + radius, radius + radius);
            myPen.Dispose();
        }
    }
    public class Hole : CircluarButton
    {
        public bool Occupied { get; set; }
        public bool Selectable { get; set; }
        public bool Selected { get; set; }
        public int Row { get; set; }
        public int Pos { get; set; }
    }
    public partial class Form1 : Form
    {
        List<Hole> holes = new List<Hole>();
        public Form1()
        {
            InitializeComponent();
            int row = 0;
            int length = 1;
            int cutoff = 10;
            int size = 100;
            for (int i = 0; i < size; i++)
            {
                if (i % cutoff == 0) {
                    row++;
                    length = 1;
                }
                Hole test = new Hole();
                test.Width = 40;
                test.Row = row;
                test.Occupied = true;
                test.Pos = length;
                test.Height = 40;
                test.Left = (length * 40) - 40;
                test.BackColor = Color.Black;
                test.Top = (row * 40) - 40;
                test.Click += Test_Click;
                this.Controls.Add(test);
                holes.Add(test);
                length++;
            }
        }

        public void Test_Click(object sender, EventArgs e)
        {
            Hole clicked = (Hole)sender;
            if (clicked.Occupied == false || clicked.Selectable == false && clicked.Selected == false && holes.FindAll(c => c.Selected == true).ToArray().Length > 0)
            {
                return;
            }
            else
            {
                if (clicked.Selected == true)
                {
                    clicked.BackColor = Color.Black;
                    clicked.Selected = false;
                    List<Hole> toChange = new List<Hole>();
                    toChange.Add(holes.Find(c => c.Row == clicked.Row && c.Pos == clicked.Pos - 2 && c.Occupied == true)); // links
                    toChange.Add(holes.Find(c => c.Row == clicked.Row && c.Pos == clicked.Pos + 2 && c.Occupied == true)); // rechts
                    toChange.Add(holes.Find(c => c.Row == clicked.Row + 2 && c.Pos == clicked.Pos && c.Occupied == true)); // omlaag
                    toChange.Add(holes.Find(c => c.Row == clicked.Row - 2 && c.Pos == clicked.Pos && c.Occupied == true)); // omhoog
                    foreach (Hole i in toChange)
                    {
                        if (i == null)
                        {
                            continue;
                        }
                        i.BackColor = Color.Black;
                        i.Selectable = false;
                    }
                }
                else if (clicked.Selectable == true)
                {
                    Hole toChange = holes.Find(c => c.Selected == true);
                    toChange.Occupied = false;
                    toChange.Selected = false;
                    toChange.BackColor = Color.Transparent;
                    clicked.Selectable = false;
                    clicked.BackColor = Color.Black;
                    var thing = holes.FindAll(c => c.Selectable == true);
                    foreach (Hole i in thing)
                    {
                        if (i == null)
                        {
                            continue;
                        }
                        i.BackColor = Color.Black;
                        i.Selectable = false;
                    }
                }
                else
                {
                    clicked.BackColor = Color.Blue;
                    clicked.Selected = true;
                    List<Hole> toChange = new List<Hole>();
                    toChange.Add(holes.Find(c => c.Row == clicked.Row && c.Pos == clicked.Pos - 2 && c.Occupied == true)); // links
                    toChange.Add(holes.Find(c => c.Row == clicked.Row && c.Pos == clicked.Pos + 2 && c.Occupied == true)); // rechts
                    toChange.Add(holes.Find(c => c.Row == clicked.Row + 2 && c.Pos == clicked.Pos && c.Occupied == true)); // omlaag
                    toChange.Add(holes.Find(c => c.Row == clicked.Row - 2 && c.Pos == clicked.Pos && c.Occupied == true)); // omhoog
                    foreach (Hole i in toChange)
                    {
                        if (i == null)
                        {
                            continue;
                        }
                        i.BackColor = Color.Yellow;
                        i.Selectable = true;
                    }
                }
            }
        }
    }
}
