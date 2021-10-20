using System;
using System.Drawing;
using System.Windows.Forms;

namespace Traffic_Simulator
{

    public class Car
    {


        public PictureBox photo;
        public Timer timer;
        public Timer checker;
        Point furthestPointAvailable;
        Road r;
        bool hasLeftRoad = false;
        double eta;
        bool hasRotated = false;
        static int ran = 5;
        int road;
        int hypotheticalRoadIndex;
        int destRoad;
        public bool b;
        bool hasGoneToDestination;

        public Car(int road, Road r)
        {
            this.road = road;
            this.hypotheticalRoadIndex = road;
            this.InitPhoto();
            this.SetDestinationRoad();
            this.InitTimer();

            this.r = r;

            checker = new Timer();
            checker.Interval = 100;
            checker.Enabled = true;
            checker.Start();
            checker.Tick += delegate (object s, EventArgs a)
            {
                DriveCar();
            };
            this.InitFurthestPointAvailable();
        }
        public bool HasRotated
        {
            get
            {
                return hasRotated;
            }
        }
        public double ETA
        {
            get
            {
                return eta;
            }
            set
            {
                eta = value;
            }
        }
        public bool HasGoneToDestination
        {
            get
            {
                return hasGoneToDestination;
            }
        }
        private bool HasReachedzebra
        {
            get
            {
                if (road == 1)
                {
                    return (photo.Location.Y >= MalemNumerus.z1.Y);
                }
                else if (road == 2)
                {
                    return (photo.Location.X <= MalemNumerus.z2.X);
                }
                else if (road == 3)
                {
                    return (photo.Location.Y <= MalemNumerus.z3.Y);
                }
                else if (road == 4)
                {
                    return (photo.Location.X >= MalemNumerus.z4.X);
                }

                return false;
            }
        }
        private bool HasReachedCrossroad
        {
            get
            {
                if (road == 1)
                {
                    if (destRoad == 2)
                    {
                        return (this.photo.Location.Y > MalemNumerus.left1.Y);
                    }
                    if (destRoad == 4)
                    {
                        return (this.photo.Location.Y > MalemNumerus.right1.Y);
                    }
                }
                else if (road == 2)
                {
                    if (destRoad == 1)
                    {
                        return (this.photo.Location.X < MalemNumerus.right2.X);
                    }
                    else if (destRoad == 3)
                    {
                        return (this.photo.Location.X < MalemNumerus.left2.X);
                    }
                }
                else if (road == 3)
                {
                    if (destRoad == 2)
                    {
                        return (this.photo.Location.Y < MalemNumerus.right3.Y);
                    }
                    else if (destRoad == 4)
                    {
                        return (this.photo.Location.Y < MalemNumerus.left3.Y);
                    }
                }
                else if (road == 4)
                {
                    if (destRoad == 1)
                    {
                        return (this.photo.Location.X > MalemNumerus.left4.X);
                    }
                    else if (destRoad == 3)
                    {
                        return (this.photo.Location.X > MalemNumerus.right4.X);
                    }
                }
                return false;
            }
        }
        private void InitFurthestPointAvailable()
        {
            switch (this.road)
            {
                case 1:
                    furthestPointAvailable = new Point(MalemNumerus.z1.X, MalemNumerus.z1.Y);
                    break;
                case 2:
                    furthestPointAvailable = new Point(MalemNumerus.z2.X, MalemNumerus.z2.Y);
                    break;
                case 3:
                    furthestPointAvailable = new Point(MalemNumerus.z3.X, MalemNumerus.z3.Y);
                    break;
                case 4:
                    furthestPointAvailable = new Point(MalemNumerus.z4.X, MalemNumerus.z4.Y);
                    break;
                default:
                    break;

            }
        }
        private void InitPhoto()
        {
            photo = new PictureBox();
            //photo.Size = new Size(80, 40);
            Random gen = new Random();
            ran += gen.Next(0, 18)*7;
            ran %= 18;
            photo.Image = Image.FromFile(Paths.carPhotosPath + ran + ".png");
            photo.BackColor = Color.Transparent;

            photo.SizeMode = PictureBoxSizeMode.StretchImage;
            SetOrientation();
            SetLocation();

        }
        public void SetOrientation()
        {
            switch (road)
            {
                case 1:
                    photo.Size = new Size(40, 80);
                    photo.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                    break;
                case 2:
                    photo.Size = new Size(80, 40);
                    photo.Image.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;
                case 3:
                    photo.Size = new Size(40, 80);
                    photo.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 4:
                    photo.Size = new Size(80, 40);
                    photo.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
            }
        }
        private void SetLocation()
        {
            switch (road)
            {
                case 1:
                    photo.Location = new Point(MalemNumerus.sp1.X, MalemNumerus.sp1.Y);
                    break;
                case 2:
                    photo.Location = new Point(MalemNumerus.sp2.X, MalemNumerus.sp2.Y);
                    break;
                case 3:
                    photo.Location = new Point(MalemNumerus.sp3.X, MalemNumerus.sp3.Y);
                    break;
                case 4:
                    photo.Location = new Point(MalemNumerus.sp4.X, MalemNumerus.sp4.Y);
                    break;
            }
        }
        private void SetDestinationRoad()
        {
            destRoad = road;
            Random gen = new Random();
            while (destRoad == road)
            {
                destRoad += gen.Next(1, 5);
                destRoad %= 4;
                destRoad += 1;
            }
        }
        private void InitTimer()
        {
            timer = new Timer();
            timer.Interval = MalemNumerus.interval;


            timer.Tick += delegate (object sender, EventArgs e)
            {

                GoStraight(this.hypotheticalRoadIndex);

                if (HasReachedCrossroad && !hasGoneToDestination)
                {
                    GoToDestination();
                    hypotheticalRoadIndex = destRoad + 4;
                }

            };
        }
        private void GoStraight(int hypotheticalRoadIndex)
        {
            if (hypotheticalRoadIndex == 1 || hypotheticalRoadIndex == 7)
            {
                this.photo.Location = new Point(photo.Location.X, photo.Location.Y + MalemNumerus.speed);
            }
            else if (hypotheticalRoadIndex == 2 || hypotheticalRoadIndex == 8)
            {
                this.photo.Location = new Point(photo.Location.X - MalemNumerus.speed, photo.Location.Y);
            }
            else if (hypotheticalRoadIndex == 3 || hypotheticalRoadIndex == 5)
            {
                this.photo.Location = new Point(photo.Location.X, photo.Location.Y - MalemNumerus.speed);
            }
            else if (hypotheticalRoadIndex == 4 || hypotheticalRoadIndex == 6)
            {
                this.photo.Location = new Point(photo.Location.X + MalemNumerus.speed, photo.Location.Y);
            }
        }
        public int Road
        {
            get
            {
                return road;
            }
            set
            {
                road = value;
            }
        }
        public int DestRoad
        {
            get
            {
                return destRoad;
            }
            set
            {
                destRoad = value;
            }
        }
        public void RotateLeft()
        {
            switch (road)
            {
                case 1:
                    photo.Size = new Size(80, 40);
                    photo.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 2:
                    photo.Size = new Size(40, 80);
                    photo.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                    break;
                case 3:
                    photo.Size = new Size(80, 40);
                    photo.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 4:
                    photo.Size = new Size(40, 80);
                    photo.Image.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
            }
            hasRotated = true;
        }
        public void RotateRight()
        {
            switch (road)
            {
                case 1:
                    photo.Size = new Size(80, 40);
                    photo.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 2:
                    photo.Size = new Size(40, 80);
                    photo.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 3:
                    photo.Size = new Size(80, 40);
                    photo.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 4:
                    photo.Size = new Size(40, 80);
                    photo.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;


            }

            hasRotated = true;
        }
        public void StartMoving()
        {
            timer.Enabled = true;
            timer.Start();

            checker.Enabled = true;
            checker.Start();
        }
        public void StopMoving()
        {
            timer.Enabled = false;
            timer.Stop();

            checker.Enabled = false;
            checker.Stop();
        }
        private void GoToDestination()
        {
            if ((this.road % 4) + 1 == destRoad) RotateLeft();
            else if ((this.road + 2) % 4 + 1 == destRoad) RotateRight();

            hasGoneToDestination = true;
        }
        public void DriveCar()
        {
            if (!HasReachedzebra)
            {
                StartMoving();
            }
            else if (!hasLeftRoad)
            {
                if (r.tl.State == true)
                {
                    hasLeftRoad = true;
                    StartMoving();
                }
                else
                {
                    StopMoving();
                }
            }

        }
        public bool IsOutsideForm()
        {
            switch (road)
            {
                case 1:
                    return (photo.Location.Y < - MalemNumerus.bigSide);
                case 2:
                    return (photo.Location.X > MalemNumerus.formDimension);
                case 3:
                    return (photo.Location.Y > MalemNumerus.formDimension);
                case 4:
                    return (photo.Location.X < - MalemNumerus.bigSide);

            }
            return false;
        }
    }
}
