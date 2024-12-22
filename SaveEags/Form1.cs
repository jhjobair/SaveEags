using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveEags
{
    public partial class Form1 : Form
    {

        bool goleft, goright;
        int speed = 8;
        int score = 0;
        int missed = 0;

        Random rndX = new Random();
        Random rndY = new Random();

        PictureBox splash = new PictureBox();

        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score; // show the score on Eggs Caught label
            txtMiss.Text = "Missed: " + missed; // Show the misses on Eggs Missed label
            // if the go left boolean is true AND chickens left is greater than 0
            if (goleft == true && chicken.Left > 0)
            {
                // then we move the chicken 12 pixels to the left
                chicken.Left -= 12;
                //checken image will be change to the one moving left
                chicken.Image = Properties.Resources.chicken_normal2;
            }
            //if the go right is true AND chickens width and left is less than form width
            // meaning the chicken is still within the frame of the game
            if (goright == true && chicken.Left + chicken.Width < this.ClientSize.Width)
            {
                // move the chicken 12 pixels to the right
                chicken.Left += 12;
                // change the chicken image to the one moving right
                chicken.Image = Properties.Resources.chicken_normal;
            }
            //below for loop will check the eggs dropping from the top
            // for each Control we are calling X in this form
            foreach (Control X in this.Controls)
            {
                // if X is a type of picture box AND it has the tag of Eggs
                if (X is PictureBox && X.Tag == "Eggs")
                {
                    // then move X towards the bottom
                    X.Top += speed;
                    // if the EGGS [X] reaches bottom of the screen
                    if (X.Top + X.Height > this.ClientSize.Height)
                    {
                        // if the egg hit the floor then we show the splash image
                        splash.Image = Properties.Resources.splash; // set the splash image
                        splash.Location = X.Location; // splash shows up on the same location as the egg
                        splash.Height = 59; // set the height
                        splash.Width = 60; // set the width
                        splash.BackColor = System.Drawing.Color.Transparent; // apply transparent background to the picture box
                        this.Controls.Add(splash); // add the splash picture to the form
                        X.Top = rndY.Next(80, 300) * -1; // position the eggs to a random Y location
                        X.Left = rndX.Next(5, this.ClientSize.Width - X.Width); // position the eggs to a random X location
                        missed++; // add 1 to the missed integer
                        chicken.Image = Properties.Resources.chicken_hurt; // change the chicken image to hurt image
                    }
                    // if the eggs bound with the chicken image
                    // if both picture boxes collide
                    if (X.Bounds.IntersectsWith(chicken.Bounds))
                    {
                        X.Top = rndY.Next(100, 300) * -1; // position the eggs to a random Y location
                        X.Left = rndX.Next(5, this.ClientSize.Width - X.Width); // position the eggs to a random X location
                        score++; // add 1 to the score
                    }
                    // if the score is equals to or greater than 20
                    if (score >= 20)
                    {
                        speed = 16; // increase the eggs speed to 20
                    }
                    // if the missed number is greater than 5
                    // we need to stop the game
                    if (missed > 5)
                    {
                        GameTimer.Stop(); // stop the game timer
                        // show the message box to say game is over. 
                        MessageBox.Show("Game Over!! We lost good Eggs" + "\r\n" + "Click OK to restart");
                        // once the players clicks OK we restart the game again
                        RestartGame();
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                // if the left key is pressed change the go left to true
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                // if the right key is pressed change the go right to true
                goright = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                // if the left key is up then change the go left to false
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                // if the right key is up then change the go right to false
                goright = false;
            }
        }

        private void txtMiss_Click(object sender, EventArgs e)
        {

        }

        private void txtScore_Click(object sender, EventArgs e)
        {

        }

        public void RestartGame()
        {
            // check all of the controls with this loop
            // create a control called X and check it in the form components
            foreach (Control X in this.Controls)
            {
                /// if X is a picture box and it has a tag of Eggs
                if (X is PictureBox && X.Tag == "Eggs")
                {
                    // we move it to top of the screen
                    X.Top = rndY.Next(80, 300) * -1; // give it a random y location
                    X.Left = rndX.Next(5, this.ClientSize.Width - X.Width); // give it a random x location
                }
            }
            chicken.Left = this.ClientSize.Width / 2; // reset the chicken to middle of the form
            chicken.Image = Properties.Resources.chicken_normal2; // change the chicken picture to face left
            score = 0; // set score to 0
            missed = 0; // set missed to 0
            speed = 8; // set speed to 8
            goleft = false; // set go left to false
            goright = false; // set go right to false
            GameTimer.Start(); // start the game timer

        }
    }
}

