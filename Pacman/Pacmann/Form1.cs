using Pacmann.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacmann
{
    public partial class Form1 : Form
    {
        Timer timer;
        Pacman pacman;
        List<Prepreka> prepreki;
        Prepreka prep;
        static readonly int TIMER_INTERVAL = 100;
        static readonly int WORLD_WIDTH = 10;
        static readonly int WORLD_HEIGHT = 15;
        Image foodImage;
        bool[][] foodWorld;

        public Form1()
        {
            InitializeComponent();  
            // Vcituvanje na slika od resursi
            foodImage = Resources.peach;
            DoubleBuffered = true;
            
            timer = new Timer();
            timer.Interval = TIMER_INTERVAL;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            newGame();
        }

        public void newGame()
        {
            pacman = new Pacman();
            prep = new Prepreka() { X = 160,Y = 120 };
            prepreki = new List<Prepreka>();
            
            this.Width = Pacman.Radius * 2 * (WORLD_WIDTH + 1);
            this.Height = Pacman.Radius * 2 * (WORLD_HEIGHT + 1);
            // овде кодот за иницијализација на матрицата foodWorld
            foodWorld = new bool[WORLD_WIDTH][];
            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                foodWorld[i] = new bool[WORLD_HEIGHT];
                for (int j = 0; j < WORLD_HEIGHT; j++)
                    foodWorld[i][j] = true;
            }
            // овде кодот за иницијализација и стартување на тајмерот
            
            while(prepreki.Count!=5)
                generatePrepreki();

        }

        public void generatePrepreki() {
            Random rnd = new Random();
            int[] X_vrednosti = new[] { 40, 80,120,160,200,240,280,320,360,400, 440, 480, 520, 560 };
            int[] Y_vrednosti = new[] { 40, 80, 120, 160, 200, 240};

            
                Prepreka p = new Prepreka();
                p.X = X_vrednosti[rnd.Next(X_vrednosti.Length - 1)];
                p.Y = Y_vrednosti[rnd.Next(Y_vrednosti.Length - 1)];
                if (prepreki.Count > 0) {
                    foreach (Prepreka pp in prepreki)
                    {
                        if (p.X - 40 == pp.X || p.X + 40 ==pp.X || p.X==pp.X) {
                        return;
                    }
                        if (p.Y==pp.Y || p.Y>pp.Y && p.Y<pp.Y-80) {
                        return;                        }
                    }        
                }
                prepreki.Add(p);
            
        }

 

        void timer_Tick(object sender, EventArgs e)
        {
            // овде вашиот код
          
            for (int i = 0; i < WORLD_HEIGHT; i++)
            {
                for (int j = 0; j < WORLD_WIDTH; j++)
                {
                    if (pacman.Y % 40 == 0 && pacman.X % 40 == 0)
                    {
                        foodWorld[pacman.Y/40][pacman.X / 40] = false;
                    }
                }
            }
            progressBar1.Value = count();
            toolStripStatusLabel2.Text = (progressBar1.Value/1.2).ToString(".0") + "%";
            if (progressBar1.Value == 120) {
                timer.Stop();
                MessageBox.Show("Completed","You win!");
                return;
            }


     
            if (checkValid())
            {
                pacman.Move(10, 20);
            }
            else
                pacman.Move(0, 0);
            pacman.mouth = !pacman.mouth;
            Invalidate();
        }

        public bool checkValid() {
            
              foreach (Prepreka prep in prepreki) { 
            if (pacman.X - 40 == prep.X && pacman.nasoka == Pacman.Nasoka.Levo &&
                (pacman.Y >prep.Y-40 && pacman.Y < prep.Y+120))
            {
                return false;
            }
            else if (pacman.X + 40 == prep.X && pacman.nasoka == Pacman.Nasoka.Desno &&
                (pacman.Y > prep.Y - 40 && pacman.Y < prep.Y + 120))
                return false;
            else if (pacman.Y == prep.Y+120 && pacman.nasoka == Pacman.Nasoka.Gore &&
                (pacman.X > prep.X-40 && pacman.X < prep.X+40))
                return false;
            else if (pacman.Y == prep.Y - 40 && pacman.nasoka == Pacman.Nasoka.Dolu &&
                (pacman.X > prep.X - 40 && pacman.X < prep.X + 40))
                return false;
            }
            return true;
        }


        public int count() {
            int c = 0;
            for (int i = 0; i < WORLD_HEIGHT; i++)
            {
                for (int j = 0; j < WORLD_WIDTH; j++)
                {
                    if (foodWorld[j][i] == false)
                        c++;
                }
            }

            return c;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // не заборавајте да го додадете настанот на самата форма
            // вашиот код овде
            if (e.KeyCode == Keys.Up)
                 pacman.ChangeDirection(Pacman.Nasoka.Gore);
            if (e.KeyCode == Keys.Left)
                pacman.ChangeDirection(Pacman.Nasoka.Levo);
            if (e.KeyCode == Keys.Down)
                pacman.ChangeDirection(Pacman.Nasoka.Dolu);
            if (e.KeyCode == Keys.Right)
                pacman.ChangeDirection(Pacman.Nasoka.Desno);
;

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            for (int i = 0; i < foodWorld.Length; i++)
            {
                for (int j = 0; j < foodWorld[i].Length; j++)
                {
                    if (foodWorld[i][j])
                    {
                        g.DrawImageUnscaled(foodImage, j * Pacman.Radius * 2 + (Pacman.Radius * 2 - foodImage.Height) / 2, i * Pacman.Radius * 2 + (Pacman.Radius * 2 - foodImage.Width) / 2);
                    }
                }
            }
          
            foreach (Prepreka p in prepreki) {
               p.draw(e.Graphics);
            }
            pacman.Draw(g);
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
