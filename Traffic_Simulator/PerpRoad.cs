using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Traffic_Simulator
{
    public class PerpRoad : Form
    {
        SpaceRoad sr1, sr2;
        Timer spaceWatch;
        Timer spaceRoadRunner;
        Timer spaceManager;
        SoundPlayer imperialMarch;
        public PerpRoad()
        {
            this.Size = new Size(750, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Visible = false;
            this.Icon = new Icon(Paths.spaceRoadIconPath);
            this.Text = "Space Road Simulator";
            this.BackgroundImage = Image.FromFile(Paths.spaceRoadBackgroundPath);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Black;

            this.InitSpaceWatch();
 

            sr1 = new SpaceRoad(1, MalemNumerus.mean, MalemNumerus.period);
            sr2 = new SpaceRoad(2, MalemNumerus.mean, MalemNumerus.period);

            InitSpaceWatch();

            AddSpaceTrafficLight(sr1.stl);
            AddSpaceTrafficLight(sr2.stl);

            AddAllSpaceships(sr1);
            AddAllSpaceships(sr2);
            sr1.stl.ChangeStateTo(true);
            sr2.stl.ChangeStateTo(false);

            InitSpaceRoadRunner();
            InitSpaceManager();


            this.KeyUp += delegate (object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.S)
                    ShutUp();
            };
            
        }
        private void InitSpaceManager()
        {
            spaceManager = new Timer();
            spaceManager.Interval = 7000;
            spaceManager.Enabled = true;
            spaceManager.Start();
            spaceManager.Tick += delegate (object sender, EventArgs a)
            {
                ManageTrafficLights();
            };
        }
        private void InitSpaceRoadRunner()
        {
            spaceRoadRunner = new Timer();
            spaceRoadRunner.Interval = 1;
            spaceRoadRunner.Enabled = true;
            spaceRoadRunner.Start();
            spaceRoadRunner.Tick += delegate (object sender, EventArgs e)
            {
                sr1.DriveSpaceshipsOnSpaceroad();
                sr2.DriveSpaceshipsOnSpaceroad();
            };
        }
        private void InitSpaceWatch()
        {
            spaceWatch = new Timer();
            spaceWatch.Interval = 1;
            spaceWatch.Enabled = true;
            spaceWatch.Start();
            spaceWatch.Tick += delegate (object sender, EventArgs e)
            {
                MalemNumerus.spaceTimeElapsed++;
            };
        }
        private void AddSpaceTrafficLight(SpaceTrafficLight s)
        {
            this.Controls.Add(s.redLight);
            this.Controls.Add(s.greenLight);
            
        }
        private void AddAllSpaceships(SpaceRoad sr)
        {
            for (int i = 0; i < sr.NbSpaceships; i++)
                AddSpaceship(sr.spaceships[i]);

        }
        private void AddSpaceship(Spaceship c)
        {
            this.Controls.Add(c.photo);
            c.StopMoving();
        }
        private async void ManageTrafficLights()
        {
            if(sr1.stl.State == true)
            {
                sr1.stl.ChangeStateTo(false);
                await Task.Delay(2000);
                sr2.stl.ChangeStateTo(true);
            }
            else if(sr2.stl.State)
            {
                sr2.stl.ChangeStateTo(false);
                await Task.Delay(2000);
                sr1.stl.ChangeStateTo(true);
            }

        }
        public void InitImperialMarch()
        {
            imperialMarch = new SoundPlayer(Paths.imperialMarch);
            imperialMarch.PlayLooping();
        }
        public void ShutUp()
        {
            imperialMarch.Stop();
        }

    }
}
