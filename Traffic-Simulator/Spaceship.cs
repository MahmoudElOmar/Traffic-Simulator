using System;
using System.Drawing;
using System.Windows.Forms;

namespace Traffic_Simulator
{
    public class Spaceship
    {
        private int road;
        private double eta;
        private Timer timer;
        private Timer spaceChecker;
        private bool hasLeftSpaceRoad = false;
        private bool hasGoneToDestination = false;
        public PictureBox photo;
        public SpaceRoad sr;
        public Spaceship(int road, SpaceRoad sr) 
        {
            this.road = road;
            this.sr = sr;
            this.InitPhoto();
            this.InitTimer();
            spaceChecker = new Timer();
            spaceChecker.Interval = 100;
            spaceChecker.Enabled = true;
            spaceChecker.Start();
            spaceChecker.Tick += delegate (object sender, EventArgs e)
            {
                  DriveSpaceship();
            };
        }
        private void GoStraight()
        {
            if (road == 1)
            {
                photo.Location = new Point(photo.Location.X + MalemNumerus.speed, photo.Location.Y);
            }
            else
            {
                if (photo.Location.Y >= MalemNumerus.ssp1.Y)
                {
                    photo.Location = new Point(photo.Location.X + MalemNumerus.speed, photo.Location.Y);
                    if (!hasGoneToDestination)
                        hasGoneToDestination = true;
                }
                else
                    photo.Location = new Point(photo.Location.X, photo.Location.Y + MalemNumerus.speed);
            }
        }
        private void InitPhoto()
        {
            photo = new PictureBox();
            photo.Size = new Size(110, 110);
            Random gen = new Random();
            photo.Image = Image.FromFile(Paths.spaceshipPhotoPath+gen.Next(1,12)+".png");
            photo.BackColor = Color.Transparent;
            photo.SizeMode = PictureBoxSizeMode.StretchImage;
            SetLocation();
        }
        private void InitTimer()
        {
            timer = new Timer();
            timer.Interval = MalemNumerus.spaceInterval;
            timer.Tick += delegate (object sender, EventArgs e)
            {
                GoStraight();
            };
        }
        private void SetLocation()
        {
            switch (road)
            {
                case 1:
                    photo.Location = new Point(MalemNumerus.ssp1.X, MalemNumerus.ssp1.Y);
                    break;
                case 2:
                    photo.Location = new Point(MalemNumerus.ssp2.X, MalemNumerus.ssp2.Y);
                    break;
                default: break;
            }

        }
        public int Road
        {
            get
            {
                return this.road;
            }
            set
            {
                this.road = value;
            }
        }
        public double ETA
        {
            set
            {
                this.eta = value;
            }
            get
            {
                return this.eta;
            }
        }
        public bool HasReachedTheshold
        {
            get
            {
                if (road == 1)
                {
                    return this.photo.Location.X > MalemNumerus.sz1.X;
                }
                else
                    return this.photo.Location.Y > MalemNumerus.sz2.Y;
            }
        }
        public bool HasGoneToDestination
        {
            get
            {
                return this.hasGoneToDestination;
            }
        }
        public bool IsOutSideForm()
        {
            if(road == 1)
            {
                return this.photo.Location.X < -110;
            }
            else if(road == 2)
            {
                return this.photo.Location.Y < -110;
            }
            return false;
        }
        public void DriveSpaceship()
        {
            if (!HasReachedTheshold)
                StartMoving();
            else if(!hasLeftSpaceRoad)
            {
                if (sr.stl.State)
                {
                    hasLeftSpaceRoad = true;
                    StartMoving();
                }
                else
                {
                    StopMoving();
                }
            }
        }
        public void StartMoving()
        {
            timer.Enabled = true;
            timer.Start();
        }
        public void StopMoving()
        {
            timer.Enabled = false;
            timer.Stop();

            spaceChecker.Enabled = false;
            spaceChecker.Stop();
        }
    }
}
