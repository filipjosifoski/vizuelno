using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacmann
{
    public class Prepreka
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Prepreka()
        {
            Width = 40;
            Height = 120;
        }

        public void draw(Graphics g)
        {
            Pen p = new Pen(Color.Red,4);
            Brush b = new SolidBrush(Color.Red);
            g.DrawRectangle(p, X, Y, Width, Height);
            g.FillRectangle(b, X, Y, Width, Height);
           
            b.Dispose();
            p.Dispose();
        }
    }
}
