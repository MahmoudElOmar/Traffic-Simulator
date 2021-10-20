using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Traffic_Simulator
{
    public class Crossroad : Form
    {
        Road r1, r2, r3, r4;
        Timer roadRunner, manager;
        SoundPlayer ohFortuna;
        SoundPlayer radio;
        int[] nbCarsOnRoads;
        PictureBox background;
        int max;
        int maxIndex;
        public Crossroad()
        {
            this.Size = new Size(MalemNumerus.formDimension, MalemNumerus.formDimension);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Visible = false;
            this.Icon = new Icon(Paths.crossroadIconPath);
            this.Text = "Crossroad Simulator";
            this.BackgroundImage = Image.FromFile(Paths.crossroadBackgroundPath);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Gray;


          


            this.InitRoads();
            this.InitWatch();
            this.InitRoadRunner();
            this.InitManager();

            nbCarsOnRoads = new int[4];

            radio = new SoundPlayer(Paths.radioPath);
            ohFortuna = new SoundPlayer(Paths.carPhotosPath);

            this.AddAllCars(r1);
            this.AddAllCars(r2);
            this.AddAllCars(r3);
            this.AddAllCars(r4);

            //r4.tl.ChangeStateTo(null);

        }
        private void InitRoads()
        {
            r1 = new Road(1, MalemNumerus.mean, MalemNumerus.period);
            r2 = new Road(2, MalemNumerus.mean, MalemNumerus.period);
            r3 = new Road(3, MalemNumerus.mean, MalemNumerus.period);
            r4 = new Road(4, MalemNumerus.mean, MalemNumerus.period);

            this.AddTrafficLight(r1.tl);
            this.AddTrafficLight(r2.tl);
            this.AddTrafficLight(r3.tl);
            this.AddTrafficLight(r4.tl);
        }
        private void InitWatch()
        {
            roadRunner = new Timer();
            roadRunner.Interval = 1;
            roadRunner.Enabled = true;
            roadRunner.Tick += delegate (object sender, EventArgs e)
            {
                MalemNumerus.timeElapsed++;
                
            };
            roadRunner.Start();
        }
        private void TerminatePanicMode()
        {
            r1.PanicMode = false;
            this.BackgroundImage = Image.FromFile(Paths.crossroadBackgroundPath);
            ohFortuna.Stop();
            SwitchPanicSpeeds();
        }
        private void InitRoadRunner()
        {
            roadRunner = new Timer();
            roadRunner.Interval = 1;
            roadRunner.Enabled = true;
            roadRunner.Tick += delegate (object sender, EventArgs e)
            {
                r1.DriveCarsOnRoad();
                r2.DriveCarsOnRoad();
                r3.DriveCarsOnRoad();
                r4.DriveCarsOnRoad();

            };
            roadRunner.Start();
        }
        private void AddTrafficLight(TrafficLight tl)
        {
            this.Controls.Add(tl.redLight);
            this.Controls.Add(tl.yellowLight);
            this.Controls.Add(tl.greenLight);
        }
        private void AddCar(Car c)
        {
            this.Controls.Add(c.photo);
        }
        private void AddAllCars(Road r)
        {
            for(int i=0;i<r.NbCars;i++)
            {
                this.AddCar(r.cars[i]);
                r.cars[i].StopMoving();
            }
        }
        private void InitManager()
        {
            manager = new Timer();
            manager.Interval = 5000;
            manager.Enabled = true;
            manager.Tick += delegate (object sender, EventArgs e)
            {
                ManageTrafficLights();
            };
            manager.Start();
        }
        private async void ManageTrafficLights()
        {
    
            if(r1.PanicMode)
            {
                if (AmbulanceHasLeft(r1.cars[r1.AmbulanceCarIndex]))
                    TerminatePanicMode();
                else
                {
                    r2.tl.ChangeStateTo(false);
                    r3.tl.ChangeStateTo(false);
                    r4.tl.ChangeStateTo(false);
                    await Task.Delay(3000);
                    r1.tl.ChangeStateTo(true);
                }
                return;
            }
            UpdateMaxIndex();
            AwakenChosenOne();
        }
        private void SwitchPanicSpeeds()
        {
            int aux = MalemNumerus.speed;
            MalemNumerus.speed = MalemNumerus.panicSpeed;
            MalemNumerus.panicSpeed = aux;

            aux = MalemNumerus.interval;
            MalemNumerus.interval = MalemNumerus.panicInterval;
            MalemNumerus.panicInterval = aux;
        }
        private bool AmbulanceHasLeft(Car c)
        {
            return c.photo.Location.X > MalemNumerus.formDimension;
        }
        private async void AwakenChosenOne()
        {
            switch (maxIndex)
            {
                case 1:
                    if (r2.tl.State == true)
                    {
                        r2.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r2.tl.ChangeStateTo(false);
                    }
                    else if (r3.tl.State == true)
                    {
                        r3.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r3.tl.ChangeStateTo(false);
                    }
                    else if (r4.tl.State == true)
                    {
                        r4.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r4.tl.ChangeStateTo(false);
                    }
                    r2.tl.ChangeStateTo(false);
                    r3.tl.ChangeStateTo(false);
                    r4.tl.ChangeStateTo(false);
                    r1.tl.ChangeStateTo(true);
                    break;
                case 2:
                    if (r1.tl.State == true)
                    {
                        r1.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r1.tl.ChangeStateTo(false);
                    }
                    else if (r3.tl.State == true)
                    {
                        r3.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r3.tl.ChangeStateTo(false);
                    }
                    else if (r4.tl.State == true)
                    {
                        r4.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r4.tl.ChangeStateTo(false);
                    }
                    r1.tl.ChangeStateTo(false);
                    r3.tl.ChangeStateTo(false);
                    r4.tl.ChangeStateTo(false);
                    r2.tl.ChangeStateTo(true);
                    break;
                case 3:
                    if (r2.tl.State == true)
                    {
                        r2.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r2.tl.ChangeStateTo(false);
                    }
                    else if (r1.tl.State == true)
                    {
                        r1.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r1.tl.ChangeStateTo(false);
                    }
                    else if (r4.tl.State == true)
                    {
                        r4.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r4.tl.ChangeStateTo(false);
                    }
                    r2.tl.ChangeStateTo(false);
                    r1.tl.ChangeStateTo(false);
                    r4.tl.ChangeStateTo(false);
                    r3.tl.ChangeStateTo(true);
                    break;
                case 4:
                    if (r2.tl.State == true)
                    {
                        r2.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r2.tl.ChangeStateTo(false);
                    }
                    else if (r3.tl.State == true)
                    {
                        r3.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r3.tl.ChangeStateTo(false);
                    }
                    else if (r1.tl.State == true)
                    {
                        r1.tl.ChangeStateTo(null);
                        await Task.Delay(MalemNumerus.delay);
                        r1.tl.ChangeStateTo(false);
                    }
                    r2.tl.ChangeStateTo(false);
                    r3.tl.ChangeStateTo(false);
                    r1.tl.ChangeStateTo(false);
                    r4.tl.ChangeStateTo(true);
                    break;
            }
        }
        private void UpdateMaxIndex()
        {
            nbCarsOnRoads[0] = r1.NbCarsOnRoad;
            nbCarsOnRoads[1] = r2.NbCarsOnRoad;
            nbCarsOnRoads[2] = r3.NbCarsOnRoad;
            nbCarsOnRoads[3] = r4.NbCarsOnRoad;
            max = 0;
            for(int i=0;i<nbCarsOnRoads.Length;i++)
            {
                if (nbCarsOnRoads[i] > max)
                {
                    max = nbCarsOnRoads[i];
                    maxIndex = i+1;
                }

            }
        }
        public void InitRadio()
        {
            radio.Play();
        }
        public void ShutUp()
        {
            radio.Stop();
            ohFortuna.Stop();
        }
        public void InitPanicMode()
        {
            r1.PanicMode = true;
            this.BackgroundImage = Image.FromFile(Paths.crossroadPanicBackgroundPath);
            ohFortuna = new SoundPlayer(Paths.ohFortunaPath);
            ohFortuna.Play();


            SwitchPanicSpeeds();
            r1.AmbulanceCarIndex = r1.NbCarsThatEnteredForm + 1;
            r1.cars[r1.AmbulanceCarIndex].photo.Image = Image.FromFile(Paths.ambulancePhotoPath);
            r1.cars[r1.AmbulanceCarIndex].SetOrientation();
            r1.cars[r1.AmbulanceCarIndex].DestRoad = 2;
        }
    }
}
