using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkad
{
    public partial class Form1 : Form
    {
        Player player;
        Ball ball,live;
        Hp hp;
        Tile tile;

        List<Tile> tiles = new List<Tile>();
        List<Hp> hps = new List<Hp>();
        List<Ball> lives = new List<Ball>();

        Random rand;

        bool moveLeft, moveRight, gameOver;
        int score, chanceToDropHp;

        Dictionary<int, Color> colors = new Dictionary<int, Color>()
            {
                {0, Color.Red},
                {1, Color.Green},
                {2, Color.Gold},
                {3, Color.GreenYellow},
                {4, Color.Indigo},
                {5, Color.HotPink},
                {6, Color.MediumVioletRed}//,
                //{7, Color.LightCyan}
            };

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            moveLeft = moveRight = gameOver = false;
            score = 0;
            chanceToDropHp = 50; // % szansa na drop hp
            ScoreValue.Text = score.ToString();

            ScoreValue.Parent = Canvas;
            Over_Score.Parent = Over;

            rand = new Random();

            player = new Player(
                Canvas.Width / 2 - 50, // pozycja x
                Canvas.Height - 20, // pozycja y
                100, // szerokosc
                10, // wysokosc
                Canvas.Width, //szerokosc okna
                5 // pretkosc paletki
                );

            ball = new Ball(
                player.X + player.SizeX / 2 - 8,//Canvas.Width / 2-10, // pozycja x
                player.Y - 20, // pozycja y
                3, // pretkosc na x
                -3, // pretkosc na y
                16, // srednica pilki
                Canvas.Height, //wysokosc okna
                Canvas.Width //szerokosc okna
                );

            for (int i = 3; i > 0; i--)
            {
                live = new Ball(
                    i * 20,//Canvas.Width / 2-10, // pozycja x
                    16, // pozycja y
                    0, // pretkosc na x
                    0, // pretkosc na y
                    16, // srednica pilki
                    Canvas.Height, //wysokosc okna
                    Canvas.Width //szerokosc okna
                    );
                lives.Add(live);
            }

            int k, a;

            for (int i = 0; i < colors.Count; i++)
            {
                if (i % 2 == 0)
                {
                    k = 12;
                    a = 16;
                }
                else
                {
                    k = 11;
                    a = 44;
                }

                for (int j = 0; j < k; j++)
                {
                    tile = new Tile(
                            j * 70 + a,// pozycja x
                            i * 35 + 50,// pozycja y
                            50,// szerokosc
                            20, // wysokosc
                            colors[i] // kolor plytki
                            );

                    tiles.Add(tile);
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            //moving
            Canvas.Refresh();

            if (moveLeft)
                player.Move(-1);
            else if (moveRight)
                player.Move(1);

            if (
                ball.Y + ball.Radius <= Canvas.Height)
                ball.Move(); 
            else
            {
                ball.X = player.X + player.SizeX / 2 - 8;
                ball.Y = player.Y - 16;
                lives.RemoveAt(lives.Count-1);
                if (lives.Count == 0) GameOver(); 
            }

            if (
                ball.Y + ball.Radius >= player.Y + player.SizeY
                && ball.X + ball.Radius >= player.X
                && ball.X - ball.Radius <= player.X + player.SizeX)
                ball.Vy = -ball.Vy;


            foreach (Hp hp in hps)
            {

                if (hp.Y + hp.Radius <= Canvas.Height && hp.Visible == 1)
                {
                    if (
                        hp.Y + hp.Radius >= player.Y + player.SizeY
                        && hp.X + hp.Radius >= player.X
                        && hp.X - hp.Radius <= player.X + player.SizeX)
                    {
                        hp.Visible = 0;

                        live = new Ball(
                            lives.Count * 20,
                            16, // pozycja y
                            0, // pretkosc na x
                            0, // pretkosc na y
                            16, // srednica pilki
                            Canvas.Height, //wysokosc okna
                            Canvas.Width //szerokosc okna
                        );
                        lives.Add(live);
                    }
                    else 
                        hp.Move();
                }
                else hp.Visible = 0;

            }

            foreach (Tile t in tiles)
           {
               if (
                   t.Visible == 1
                   && ball.Y + ball.Radius >= t.Y
                   && ball.Y - ball.Radius <= t.Y + t.SizeY
                   && ball.X + ball.Radius >= t.X
                   && ball.X - ball.Radius <= t.X + t.SizeX
                   )
               {
                   if (
                        Math.Abs((ball.Y + ball.Radius) - t.Y) <= 1
                        || Math.Abs((ball.Y - ball.Radius) - (t.Y + t.SizeY)) <= Math.Abs(ball.Vy)
                        )
                        ball.Vy = -ball.Vy;
                    else if (
                         Math.Abs((ball.X + ball.Radius) - t.X) <= 1
                         || Math.Abs((ball.X - ball.Radius) - (t.X + t.SizeX)) <= Math.Abs(ball.Vx)
                         )
                        ball.Vx = -ball.Vx;

                    t.Visible = 0;
                    ScoreValue.Text = (++score).ToString();

                    if (rand.Next(0, 100 / chanceToDropHp) == 0) DropHp(t.X, t.Y);


                    gameOver = true;
                    foreach (Tile ts in tiles) if (ts.Visible == 1) gameOver = false;
                    if (gameOver) GameOver();
                }
           }
        }

        private void GameOver()
        {
            Timer.Stop();
            Over_Score.Text = "Your Score: " + score.ToString();
            Over.Visible = true;
            gameOver = true;
        }

        private void DropHp(int x, int y)
        {
            hp = new Hp(
                x,
                y,
                3, // pretkosc na y
                13, // srednica pilki
                Canvas.Height, //wysokosc okna
                Canvas.Width //szerokosc okna
                );

            hps.Add(hp);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            //rendering
            player.Render(e.Graphics);
            ball.Render(e.Graphics);

            foreach (Tile t in tiles) t.Render(e.Graphics);
            foreach (Hp t in hps) t.Render(e.Graphics);
            foreach (Ball t in lives) t.Render(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P && !gameOver)
                    Timer.Start();

            if (e.KeyCode == Keys.R)
            {
                tiles.Clear();
                lives.Clear();
                Over.Visible = false;
                Initialize();
                if (Timer.Enabled == false) Timer.Start();
            }
                

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                moveLeft = true;
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                moveRight = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                moveLeft = false;
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                moveRight = false;
        }
    }
}
