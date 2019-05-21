using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacmann
{
    public class Pacman
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static int Radius { get; set; }
        public int Speed { get; set; }
        public bool mouth { get; set; }
        public Nasoka nasoka { get; set; }

        public enum Nasoka {
            Gore,
            Dolu,
            Levo,
            Desno
        }

        public Pacman() {
            X = 280;
            Y = 200;
            Radius = 20;
            Speed = Radius;
            nasoka = Nasoka.Desno;
            mouth = false;
        }

        public void ChangeDirection(Nasoka nasokaa)
        {
            nasoka = nasokaa;
        }

        public void Move(int x,int y)
        {
            if (nasoka == Nasoka.Desno) {
                if (X >= 560)
                    X = 0;
                else
                    X += x;
                
            }
            if (nasoka == Nasoka.Levo)
            {
                X -= x;
                if (X < 0)
                    X = 560;
            }
            if (nasoka == Nasoka.Gore)
            {
                Y -= x;
                if (Y < 0)
                    Y=340;
            }
            if (nasoka == Nasoka.Dolu)
            {
                Y += x;
                if (Y >= 360)
                    Y = 0;
            }
        }


        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Yellow);
            if (mouth)
            {
                if (nasoka == Nasoka.Desno) {
                    g.FillPie(b, X, Y, 2 * Radius, 2 * Radius, 30, 300);
                }
                if (nasoka == Nasoka.Levo) {
                    g.FillPie(b, X, Y, 2 * Radius, 2 * Radius, 210, 300);
                }
                if (nasoka == Nasoka.Gore) {
                    g.FillPie(b, X, Y, 2 * Radius, 2 * Radius, 300, 300);
                }
                if (nasoka == Nasoka.Dolu) {
                    g.FillPie(b, X, Y, 2 * Radius, 2 * Radius, 120, 300);
                }
            }
            else
            {
                g.FillEllipse(b, X, Y, 2 * Radius, 2 * Radius);
            }
            b.Dispose();
        }
    }
}
