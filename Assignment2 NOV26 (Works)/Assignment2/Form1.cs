using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class Assignment2Form : Form
    {
        bool start;
        Image[] backgroundImages = new Image[5];//Background images
        Image[] platformImages = new Image[3];
        int imageCounter; 
        Rectangle backgroundBox;
        Rectangle floorBox;
        Rectangle playerBox;//player box
        Rectangle[] projectileBoxes = new Rectangle[30];//projectiles (to be added)
        Random enemyGenerator = new Random();//random generator for enemies
        int numberOfEnemies;
        Random positionGenerator = new Random();
        Rectangle[] enemyBoxes;//potential enemy
        Rectangle platform;
        Rectangle platform2;
        Rectangle platform3;

        bool left, right;//true or false on left and right movement
        bool jump;//check if you jump is true
        int G = 30;//gravity
        int Force;//force when going down

        //Projectile stuff BETA
        Rectangle[] playerProjectileBox;
        float[] projectileXSpeed, projectileYSpeed;
        float rise, run;
        float hypotenuse;
        const int PROJECTILE_SPEED = 40;
        bool[] projectilesInMotion; 
        //Projectile stuff BETA

        Point cursorLocation;//cursor location

        //animations
        int[] animationIndexes;
        Rectangle[] animatedBoxes;
        Image[] animationImages;

        public Assignment2Form()
        {
            InitializeComponent();
            InitializeGraphics();
            KeyPreview = true;           
            lblError.Text = "";
            this.Cursor = new Cursor("CursorImageTargetSmall.cur"); //Change cursor Image
        }

        private void Assignment2Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                right = true;
            }
            else if (e.KeyCode == Keys.A)
            {
                left = true;
            }

            if (jump != true)
            {
                if (e.KeyCode == Keys.Space)
                {
                    jump = true;
                    Force = G;
                }
            }
        }

        private void Assignment2Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                right = false;
            }
            if (e.KeyCode == Keys.A)
            {
                left = false;
            }
        }

        private void Assignment2Form_MouseClick(object sender, MouseEventArgs e)
        {
            
            CreateNewProjectile();
            
        }

      

        private void tmrMovement_Tick(object sender, EventArgs e)
        {

            colissionPart1();



            if (right == true)
            {
                playerBox.X += 5;
            }
            if (left == true)
            {
                playerBox.X -= 5;
            }

           
            if (jump == true)
            {
                //Falling if in the air/if the player has jumped
                playerBox.Y -= Force;
                Force -= 1;
            }

            if (playerBox.Y + playerBox.Height >= backgroundBox.Height)
            {
                playerBox.Y = backgroundBox.Height - playerBox.Height; //stop falling at bottom
                jump = false;
            }
            else
            {
                playerBox.Y += 5; //Falling

            }

            MoveProjectile();

            int currentPlayerY = playerBox.Y;

            if (playerBox.X > ClientSize.Width && imageCounter < backgroundImages.Length - 1)
            {
                imageCounter = imageCounter + 1;
                for (int i = 0; i < projectilesInMotion.Length; i++)
                {
                    projectilesInMotion[i] = false;
                }
                
                playerBox.Location = new Point(0, currentPlayerY);
            }
            else if (playerBox.X < 0 && imageCounter > 0)
            {
                imageCounter = imageCounter - 1;
                for (int i = 0; i < projectilesInMotion.Length; i++)
                {
                    projectilesInMotion[i] = false;
                }            
                playerBox.Location = new Point(ClientSize.Width - playerBox.Width, currentPlayerY);
            }

            if (imageCounter == 0)
            {
                if (playerBox.X < 0) // left border
                {
                    playerBox.X = 0;
                }
            }

            if (imageCounter == backgroundImages.Length - 1)
            {
                if (playerBox.X + playerBox.Width >= backgroundBox.Width)
                {
                    playerBox.X = backgroundBox.Width - playerBox.Width; //right border                               
                }
            }
            setStage();

            colissionPart2();

            Refresh(); 
        }

        void colissionPart1()
        {
            //side collision platform 1
            if (playerBox.Right > platform.Left && playerBox.Left < platform.Right - playerBox.Width && playerBox.Bottom <= platform.Bottom && playerBox.Bottom > platform.Top)
            {
                right = false;
                playerBox.X = platform.Left - playerBox.Width;

            }

            if (playerBox.Left < platform.Right && playerBox.Right > platform.Left + playerBox.Width && playerBox.Bottom <= platform.Bottom && playerBox.Bottom > platform.Top)
            {
                left = false;
                playerBox.X = platform.Right;
            }

            //platform 2
            //side collision platform 2
            if (playerBox.Right > platform2.Left && playerBox.Left < platform2.Right - playerBox.Width && playerBox.Bottom <= platform2.Bottom && playerBox.Bottom > platform2.Top)
            {
                right = false;
                playerBox.X = platform2.Left - playerBox.Width;

            }

            if (playerBox.Left < platform2.Right && playerBox.Right > platform2.Left + playerBox.Width && playerBox.Bottom <= platform2.Bottom && playerBox.Bottom > platform2.Top)
            {
                left = false;
                playerBox.X = platform2.Right;

            }

            //side collision platform 3
            if (playerBox.Right > platform3.Left && playerBox.Left < platform3.Right - playerBox.Width && playerBox.Bottom <= platform3.Bottom && playerBox.Bottom > platform3.Top)
            {
                right = false;
                playerBox.X = platform3.Left - playerBox.Width;

            }

            if (playerBox.Left < platform3.Right && playerBox.Right > platform3.Left + playerBox.Width && playerBox.Bottom <= platform3.Bottom && playerBox.Bottom > platform3.Top)
            {
                left = false;
                playerBox.X = platform3.Right;

            }

        }

        void colissionPart2()
        {
            ///////////////Platform colision 
            //Top Collision platform 1 player

            if (playerBox.Left + playerBox.Width > platform.Left && playerBox.Left + playerBox.Width < platform.Left + platform.Width + playerBox.Width && playerBox.Top + playerBox.Height >= platform.Top && playerBox.Top < platform.Top)
            {
                jump = false;
                Force = 0;
                playerBox.Y = platform.Location.Y - playerBox.Height;
            }
            //Fixing the slow fall platform 1
            if (!(playerBox.Left + playerBox.Width > platform.Left && playerBox.Left + playerBox.Width < platform.Left + platform.Width + playerBox.Width) && playerBox.Top + playerBox.Height >= platform.Top && playerBox.Top < platform.Top && !(playerBox.Bottom == platform3.Top || playerBox.Bottom == platform2.Top))
            {
                jump = true;
            }
            //Head collision platform 1
            if (playerBox.Left + playerBox.Width > platform.Left && playerBox.Left + playerBox.Width < platform.Left + platform.Width + playerBox.Width && playerBox.Top - platform.Bottom <= 10 && playerBox.Top - platform.Top > -10)
            {
                Force = -1;
            }


            //platform2
            //Top Collision platform 2 player

            if (playerBox.Left + playerBox.Width > platform2.Left && playerBox.Left + playerBox.Width < platform2.Left + platform2.Width + playerBox.Width && playerBox.Top + playerBox.Height >= platform2.Top && playerBox.Top < platform2.Top)
            {
                jump = false;
                Force = 0;
                playerBox.Y = platform2.Location.Y - playerBox.Height;
            }
            //Fixing the slow fall platform 2
            if (!(playerBox.Left + playerBox.Width > platform2.Left && playerBox.Left + playerBox.Width < platform2.Left + platform2.Width + playerBox.Width) && playerBox.Top + playerBox.Height >= platform2.Top && playerBox.Top < platform2.Top && !(playerBox.Bottom == platform.Top || playerBox.Bottom == platform3.Top))
            {
                jump = true;
            }
            //Head collision platform 2
            if (playerBox.Left + playerBox.Width > platform2.Left && playerBox.Left + playerBox.Width < platform2.Left + platform2.Width + playerBox.Width && playerBox.Top - platform2.Bottom <= 10 && playerBox.Top - platform2.Top > -10)
            {
                Force = -1;
            }
            ///////////////Platform colision 

            //platform2
            //Top Collision platform 2 player

            if (playerBox.Left + playerBox.Width > platform3.Left && playerBox.Left + playerBox.Width < platform3.Left + platform3.Width + playerBox.Width && playerBox.Top + playerBox.Height >= platform3.Top && playerBox.Top < platform3.Top)
            {
                jump = false;
                Force = 0;
                playerBox.Y = platform3.Location.Y - playerBox.Height;
            }
            //Fixing the slow fall platform 2
            if (!(playerBox.Left + playerBox.Width > platform3.Left && playerBox.Left + playerBox.Width < platform3.Left + platform3.Width + playerBox.Width) && playerBox.Top + playerBox.Height >= platform3.Top && playerBox.Top < platform3.Top && !(playerBox.Bottom == platform.Top || playerBox.Bottom == platform2.Top))
            {
                jump = true;
            }
            //Head collision platform 2
            if (playerBox.Left + playerBox.Width > platform3.Left && playerBox.Left + playerBox.Width < platform3.Left + platform3.Width + playerBox.Width && playerBox.Top - platform3.Bottom <= 10 && playerBox.Top - platform3.Top > -10)
            {
                Force = -1;
            }
            ///////////////Platform colision 
        }

        void setStage()
        {
            const int platformHeight = 120;
            const int platform2Height = 300;
            const int platform3Height = 200;

            if (imageCounter == 0)
            {
                platform = new Rectangle(100, (ClientSize.Height - platformHeight - 200), 400, platformHeight);
                platform2 = new Rectangle(500, (ClientSize.Height - platform2Height - 200), 200, platform2Height);
                platform3 = new Rectangle(700, (ClientSize.Height - platform3Height - 200), 200, platform3Height);
            }
            else if (imageCounter == 1)
            {
                platform = new Rectangle(500, (ClientSize.Height - platformHeight - 200), 400, platformHeight);
                platform2 = new Rectangle(700, (ClientSize.Height - platform2Height - 200), 200, platform2Height);
                platform3 = new Rectangle(400, (ClientSize.Height - platform3Height - 200), 200, platform3Height);
            }
            else if (imageCounter == 2)
            {
                platform = new Rectangle(400, (ClientSize.Height - platformHeight - 100) - 300, 400, platformHeight);
                platform2 = new Rectangle(200, (ClientSize.Height - platform2Height - 200), 200, platform2Height);
                platform3 = new Rectangle(700, (ClientSize.Height - platform3Height - 200), 200, platform3Height);
            }
            else if (imageCounter == 3)
            {
                platform = new Rectangle(200, (ClientSize.Height - platformHeight - 100) - 300, 400, platformHeight);
                platform2 = new Rectangle(400, (ClientSize.Height - platform2Height - 200), 200, platform2Height);
                platform3 = new Rectangle(600, (ClientSize.Height - platform3Height - 200), 200, platform3Height);
            }
            else if (imageCounter == 4)
            {
                platform = new Rectangle(100, (ClientSize.Height - platformHeight - 200), 400, platformHeight);
                platform2 = new Rectangle(500, (ClientSize.Height - platform2Height - 200), 200, platform2Height);
                platform3 = new Rectangle(300, (ClientSize.Height - platform3Height - 200) - 120, 200, platform3Height);
            }
        }

        void InitializeGraphics()
        {
            //set up rectangle: x position, y position, width, height,
            playerBox = new Rectangle(100, 100, 80, 120);

            backgroundBox = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height - 200);

            floorBox = new Rectangle(0, backgroundBox.Bottom, ClientSize.Width, ClientSize.Height - backgroundBox.Height);

            int.TryParse(txtEnemies.Text, out numberOfEnemies);
            if (!(3 <= numberOfEnemies && numberOfEnemies <= 5))
            {
                lblError.Text = "Error";
            }
            else
            {
                enemyBoxes = new Rectangle[enemyGenerator.Next(0, numberOfEnemies)];

                for (int i = 0; i < enemyBoxes.Length; i++)
                {
                    enemyBoxes[i] = new Rectangle(positionGenerator.Next(100, 100), positionGenerator.Next(100, 100), 100, 100);
                }
                lblError.Hide();


            }

            playerProjectileBox = new Rectangle[30];
            projectilesInMotion = new bool[30];
            projectileXSpeed = new float[30];
            projectileYSpeed = new float[30];

            backgroundImages[0] = Properties.Resources.Background1;
            backgroundImages[1] = Properties.Resources.Background2;
            backgroundImages[2] = Properties.Resources.Background3;
            backgroundImages[3] = Properties.Resources.Background4;
            backgroundImages[4] = Properties.Resources.Background5;

            platformImages[0] = Properties.Resources.Platform;
            platformImages[1] = Properties.Resources.Platform2;
            platformImages[2] = Properties.Resources.Platform3;
        }
        
        void CreateNewProjectile()
        {
            for (int i = 0; i < playerProjectileBox.Length; i++)
            {
                if (projectilesInMotion[i] == false)
                {
                    


                    projectilesInMotion[i] = true;

                   

                    // calculate slope vector from the shooter to the target
                    rise = cursorLocation.Y - (playerBox.Y + (playerBox.Height / 2));
                    run = cursorLocation.X - (playerBox.X + (playerBox.Width / 2));
                    hypotenuse = (float)Math.Sqrt(rise * rise + run * run);

                    //calulate the speeds for the projectile using slope vector
                    projectileXSpeed[i] = run / hypotenuse * PROJECTILE_SPEED;
                    projectileYSpeed[i] = rise / hypotenuse * PROJECTILE_SPEED;

                    playerProjectileBox[i] = new Rectangle(playerBox.X + (playerBox.Width / 2), playerBox.Y + (playerBox.Height / 2), 10, 10);

                    break; 
                }
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
           
        }

        void MoveProjectile()
        {
            for (int i = 0; i < playerProjectileBox.Length; i++)
            {
                if (projectilesInMotion[i] == true)
                {
                    
                    playerProjectileBox[i].X = playerProjectileBox[i].X + (int)projectileXSpeed[i];
                    playerProjectileBox[i].Y = playerProjectileBox[i].Y + (int)projectileYSpeed[i];

                    if (playerProjectileBox[i].Y < 0 || playerProjectileBox[i].Y > backgroundBox.Height ||
                        playerProjectileBox[i].X < 0 || playerProjectileBox[i].X > backgroundBox.Width ||
                        playerProjectileBox[i].IntersectsWith(platform) == true || playerProjectileBox[i].IntersectsWith(platform2) == true ||
                        playerProjectileBox[i].IntersectsWith(platform3) == true)
                    {
                        projectilesInMotion[i] = false;
                    }
                }
            }
        }
        


        private void Assignment2Form_MouseMove(object sender, MouseEventArgs e)
        {
            //now use cursorLocation (X,Y) where you need cursor position
            cursorLocation = e.Location; 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(backgroundImages[imageCounter], backgroundBox);           
            e.Graphics.DrawImage(Properties.Resources.Placeholder, playerBox);

            e.Graphics.DrawImage(platformImages[0], platform);
            e.Graphics.DrawImage(platformImages[1], platform2);
            e.Graphics.DrawImage(platformImages[2], platform3);

            for (int i = 0; i < playerProjectileBox.Length; i++)
            {
                if (projectilesInMotion[i] == true)
                {
                    e.Graphics.FillRectangle(Brushes.Red, playerProjectileBox[i]);
                }
            }

            if (3 <= numberOfEnemies && numberOfEnemies <= 5)
            {
                for (int i = 0; i < enemyBoxes.Length; i++)
                {
                    e.Graphics.FillRectangle(Brushes.Green, enemyBoxes[i]);
                }
            }
           

            e.Graphics.DrawImage(Properties.Resources.StoneFloor, floorBox);
        }
    }
}
