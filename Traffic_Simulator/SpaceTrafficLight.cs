using System.Drawing;
using System.Windows.Forms;

namespace Traffic_Simulator
{
    public class SpaceTrafficLight
    {
        bool state;
        int road;
        public PictureBox redLight;
        public PictureBox greenLight;
        public SpaceTrafficLight(bool state = false, int road = 1)
        {
            this.road = road;
            this.state = state;

            redLight = new PictureBox();
            greenLight = new PictureBox();

            InitPictureBox(redLight, Paths.redSpaceLightPath);
            InitPictureBox(greenLight, Paths.greenSpaceLightPath);

        }
        private void InitPictureBox(PictureBox p, string path)
        {
            p.Image = Image.FromFile(path);
            p.SizeMode = PictureBoxSizeMode.StretchImage;
            p.BackColor = Color.Transparent;
            p.Size = new Size(MalemNumerus.lightSaberBigSide, MalemNumerus.lightSaberSmallSide);
            SetLocation();
            if(road == 2)
            {
                p.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                p.Size = new Size(MalemNumerus.lightSaberSmallSide, MalemNumerus.lightSaberBigSide);
            }
            //p.Hide();
            ChangeStateTo(state);
            p.BringToFront();
        }
        public bool State
        {
            get
            {
                return this.state;
            }
        }
        private void SetOrientation()
        {
            if (road == 2)
            {
                if (state == true)
                {
                    greenLight.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    greenLight.Size = new Size(MalemNumerus.lightSaberSmallSide, MalemNumerus.lightSaberBigSide);
                }
                else
                {
                    redLight.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    redLight.Size = new Size(MalemNumerus.lightSaberSmallSide, MalemNumerus.lightSaberBigSide);
    
                }
            }
        }
        private void SetLocation()
        {
            switch (road)
            {
                case 1:
                    redLight.Location = new Point(MalemNumerus.sl1.X, MalemNumerus.sl1.Y);
                    greenLight.Location = new Point(MalemNumerus.sl1.X, MalemNumerus.sl1.Y);
                    break;
                case 2:
                    redLight.Location = new Point(MalemNumerus.sl2.X, MalemNumerus.sl2.Y);
                    greenLight.Location = new Point(MalemNumerus.sl2.X, MalemNumerus.sl2.Y);
                    break;
            }

        }
        public void ChangeStateTo(bool state)
        {
            this.state = state;

            switch(state)
            {
                case true:
                    redLight.Hide();
                    greenLight.Show();
                    break;
                case false:
                    greenLight.Hide();
                    redLight.Show();
                    break;
                default:break;
            }
        }
    }
}
