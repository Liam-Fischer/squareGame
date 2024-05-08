using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;

namespace squareGame
{
    public partial class Form1 : Form
    {
        /// <summary>
        ///  Liam R. Fischer
        ///  May 8th 2024
        ///  A simple two-player game using randoms, rectangles and key controls
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            GameInitialize();
        }
        Rectangle player1 = new Rectangle(25, 240, 15, 15);
        Rectangle player2 = new Rectangle(555, 240, 15, 15);
        Rectangle goal = new Rectangle(0, 0, 7, 7);
        Rectangle inverter = new Rectangle(200, 0, 6, 6);
        

        Rectangle top = new Rectangle(0, 0, 600, 7);
        Rectangle bottom = new Rectangle(0, 493, 600, 7);
        Rectangle leftSide = new Rectangle(0, 0, 7, 500);
        Rectangle rightSide = new Rectangle(593, 0, 7, 500);
        Rectangle leftBottom = new Rectangle(135, 350, 50, 150);
        Rectangle rightBottom = new Rectangle(415, 350, 50, 150);
        Rectangle leftTop = new Rectangle(135, 0, 50, 150);
        Rectangle rightTop = new Rectangle(415, 0, 50, 150);
        

        SoundPlayer point = new SoundPlayer(Properties.Resources.A_Tone_His_Self_1266414414__1_);
        SoundPlayer winner = new SoundPlayer(Properties.Resources.Ta_Da_SoundBible_com_1884170640);

        int player1score = 0;
        int player2score = 0;
        int playerXspeed = 4;
        int playerYspeed = 4;

        bool wPressed = false;
        bool dPressed = false;
        bool aPressed = false;
        bool sPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        bool playStart = false;
        bool invert1 = false;
        bool invert2 = false;

        Random placer = new Random();
        

        SolidBrush p1Brush = new SolidBrush(Color.Coral);
        SolidBrush p2Brush = new SolidBrush(Color.PaleTurquoise);
        SolidBrush goalBrush = new SolidBrush(Color.MediumOrchid);
        SolidBrush borderBrush = new SolidBrush(Color.Lime);
        Pen borderPen = new Pen(Color.Lime, 7);
        SolidBrush inverterBrush = new SolidBrush(Color.DeepPink);

        public void Form1_Paint(object sender, PaintEventArgs q)
        {
            //drawing characters
            q.Graphics.FillRectangle(p1Brush, player1);
            q.Graphics.FillRectangle(p2Brush, player2);
            q.Graphics.FillRectangle(goalBrush, goal);
            q.Graphics.FillRectangle(inverterBrush, inverter);
            
            q.Graphics.FillRectangle(borderBrush, top);
            q.Graphics.FillRectangle(borderBrush, bottom);
            q.Graphics.FillRectangle(borderBrush, leftSide);
            q.Graphics.FillRectangle(borderBrush, rightSide);
            q.Graphics.DrawRectangle(borderPen, leftBottom);
            q.Graphics.DrawRectangle(borderPen, rightBottom);
            q.Graphics.DrawRectangle(borderPen, leftTop);
            q.Graphics.DrawRectangle(borderPen, rightTop);

        }
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;

            }
        }
        public void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }
        public void gameTimer_Tick(object sender, EventArgs w)
        {
            MoveCharacter();

            //placing the goal 1st time
            if (goal.X != 0)
            {
                playStart = true;
            }
            if (playStart == true)
            {
                goal.X = goal.X;
                goal.Y = goal.Y;
                inverter.X = inverter.X;
                inverter.Y = inverter.Y;
            }
            

            //player 1 and goal
            if (player1.IntersectsWith(goal))
            {
                player1score++;
                p1ScoreLabel.Text = $"{player1score}";
                PointScoring();
            }
            //player 2 and goal
            if (player2.IntersectsWith(goal))
            {
                player2score++;
                p2ScoreLabel.Text = $"{player2score}";
                PointScoring();           
            }
            //players and inverter
            if (player1.IntersectsWith(inverter))
            {
                
                invert2 = true;
                invertTimer.Start();
                inverter.X = -100;
                inverter.Y = -100;
            }

            if (player2.IntersectsWith(inverter))
            {
                invert1 = true;
                invertTimer.Start();
                inverter.X = -100;
                inverter.Y = -100;
            }

            //keep this at the bottom
            Refresh();
        }
        public void MoveCharacter()
        {
            int x1 = player1.X;
            int y1 = player1.Y;
            int x2 = player2.X;
            int y2 = player2.Y;

            //moving player 1
            
            
                if (invert1 == false)
                {

                    if (dPressed == true && player1.X < this.Width - player1.Width - 7)
                    {
                        player1.X = player1.X + playerXspeed;
                    }
                    if (aPressed == true && player1.X > 7)
                    {
                        player1.X = player1.X - playerXspeed;
                    }

                    if (wPressed == true && player1.Y > 7)
                    {
                        player1.Y = player1.Y - playerYspeed;
                    }
                    if (sPressed == true && player1.Y < this.Height - player1.Height - 7)
                    {
                        player1.Y = player1.Y + playerYspeed;
                    }
                }
                else if (invert1 == true)
                {

                    if (dPressed == true && player1.X > 7)
                    {
                        player1.X = player1.X - playerXspeed;
                    }
                    if (aPressed == true && player1.X < this.Width - player1.Width - 7)
                    {
                        player1.X = player1.X + playerXspeed;
                    }
                    if (wPressed == true && player1.Y < this.Height - player1.Height - 7)
                    {
                        player1.Y = player1.Y + playerYspeed;
                    }
                    if (sPressed == true && player1.Y > 7)
                    {
                        player1.Y = player1.Y - playerYspeed;
                    }
                }
            

            if (player1.IntersectsWith(leftTop) || player1.IntersectsWith(rightTop) || player1.IntersectsWith(leftBottom) || player1.IntersectsWith(rightBottom))
            {
                player1.X = x1;
                player1.Y = y1;
            }
                //moving player 2
                if (invert2 == false)
                {
                    if (upPressed == true && player2.Y > 7)
                    {
                        player2.Y = player2.Y - playerYspeed;
                    }
                    if (downPressed == true && player2.Y < this.Height - player2.Height - 7)
                    {
                        player2.Y = player2.Y + playerYspeed;
                    }
                    if (leftPressed == true && player2.X > 7)
                    {
                        player2.X = player2.X - playerXspeed;
                    }
                    if (rightPressed == true && player2.X < this.Width - player1.Width - 7)
                    {
                        player2.X = player2.X + playerXspeed;
                    }
                }


                else if (invert2 == true)
                {
                    if (upPressed == true && player2.Y < this.Height - player2.Height - 7)
                    {
                        player2.Y = player2.Y + playerYspeed;
                    }
                    if (downPressed == true && player2.Y > 7)
                    {
                        player2.Y = player2.Y - playerYspeed;
                    }
                    if (leftPressed == true && player2.X < this.Width - player1.Width - 7)
                    {
                        player2.X = player2.X + playerXspeed;
                    }
                    if (rightPressed == true && player2.X > 7)
                    {
                        player2.X = player2.X - playerXspeed;
                    }
                }
            if (player2.IntersectsWith(leftTop) || player2.IntersectsWith(rightTop) || player2.IntersectsWith(leftBottom) || player2.IntersectsWith(rightBottom))
            {
                player2.X = x2;
                player2.Y = y2;
            }

        }
        public void PointScoring()
        {
            point.Play();
            goal.X = placer.Next(8, this.Width - 11);
            goal.Y = placer.Next(8, this.Height - 11);
            if (goal.IntersectsWith(leftTop) || goal.IntersectsWith(rightTop) || goal.IntersectsWith(rightBottom) || goal.IntersectsWith(leftBottom))
            {
                goal.X = placer.Next(8, this.Width - 11);
                goal.Y = placer.Next(8, this.Height - 11);
            }
            invert1 = false;
            invert2 = false;

            if (player2score == 11)
            {
                p2ScoreLabel.ForeColor = Color.Yellow;
                winLabel.Text = "Player 2 wins!";
                gameTimer.Stop();
                winner.Play();
            }
            else if (player1score == 11)
            {
                p1ScoreLabel.ForeColor = Color.Yellow;
                winLabel.Text = "Player 1 wins!";
                gameTimer.Stop();
                winner.Play();
            }
        }
        public void GameInitialize()
        {
            inverter.X = placer.Next(8, this.Width - 11);
            inverter.Y = placer.Next(8, this.Height - 11);

            goal.X = placer.Next(8, this.Width - 11);
            goal.Y = placer.Next(8, this.Height - 11);
        }
        private void invertTimer_Tick(object sender, EventArgs e)
        {
            inverter.X = placer.Next(8, this.Width - 11);
            inverter.Y = placer.Next(8, this.Height - 11);
            if (inverter.IntersectsWith(leftTop) || inverter.IntersectsWith(rightTop) || inverter.IntersectsWith(rightBottom) || inverter.IntersectsWith(leftBottom))
            {
                inverter.X = placer.Next(8, this.Width - 11);
                inverter.Y = placer.Next(8, this.Height - 11);
            }
                invertTimer.Stop();
        }
    }
}
