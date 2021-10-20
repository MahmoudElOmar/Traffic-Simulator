using System;
using System.Drawing;
using System.Windows.Forms;

namespace Traffic_Simulator
{
    public class TrafficLight
    {
        private bool? state;
        private int road;

        public PictureBox redLight = new PictureBox();
        public PictureBox yellowLight = new PictureBox();
        public PictureBox greenLight = new PictureBox();

        public TrafficLight(bool? state = null, int road = 1)
        {
            this.state = state;
            this.road = road;
            this.InitPictureBox(redLight, Paths.redLightPath);
            this.InitPictureBox(yellowLight, Paths.yellowLightPath);
            this.InitPictureBox(greenLight, Paths.greenLightPath);

        }
        private void SetLocation()
        {
            switch (road)
            {
                case 1:
                    redLight.Location = MalemNumerus.r1;
                    yellowLight.Location = MalemNumerus.y1;
                    greenLight.Location = MalemNumerus.g1;
                    break;
                case 2:
                    redLight.Location = MalemNumerus.r2;
                    yellowLight.Location = MalemNumerus.y2;
                    greenLight.Location = MalemNumerus.g2;
                    break;
                case 3:
                    redLight.Location = MalemNumerus.r3;
                    yellowLight.Location = MalemNumerus.y3;
                    greenLight.Location = MalemNumerus.g3;
                    break;
                case 4:
                    redLight.Location = MalemNumerus.r4;
                    yellowLight.Location = MalemNumerus.y4;
                    greenLight.Location = MalemNumerus.g4;
                    break;
                default:
                    break;
            }

        } 
        private void InitPictureBox(PictureBox p, String path)
        {

            p.Image = Image.FromFile(path);
            p.Size = new Size(17,17);
            this.SetLocation();
            p.SizeMode = PictureBoxSizeMode.StretchImage;
            p.BackColor = Color.Transparent;
            p.BringToFront();
            ChangeStateTo(state);
        }
        public bool? State
        {
            get
            {
                return this.state;
            }
        }
        public void ChangeStateTo(bool? state)
        {
            this.state = state;
            switch (state)
            {
                case false:
                    yellowLight.Hide();
                    greenLight.Hide();
                    redLight.Show();
                    break;
                case null:
                    greenLight.Hide();
                    redLight.Hide();
                    yellowLight.Show();
                    break;
                case true:
                    redLight.Hide();
                    yellowLight.Hide();
                    greenLight.Show();
                   break;                   
            }
        }

    }
}
