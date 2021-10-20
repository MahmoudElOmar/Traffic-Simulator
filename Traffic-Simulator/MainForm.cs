using System;
using System.Drawing;
using System.Windows.Forms;

namespace Traffic_Simulator
{
    public partial class MainForm : Form
    {
        PictureBox cross, perp;
        Crossroad crossroad;
        PerpRoad perproad;
        //TextBox periodTextbox, meanTextbox;
        //Label periodLabel, meanLabel;
        public MainForm()
        {
            InitializeComponent();
            this.InitMain();
            this.InitPictureBoxes();
            //this.InitLabelsAndTextboxes();
            crossroad = new Crossroad();

            cross.MouseClick += delegate (object sender, MouseEventArgs e)
            {
                crossroad = new Crossroad();
                crossroad.Visible = true;
                this.Visible = false;
                crossroad.KeyDown += delegate (object key, KeyEventArgs s)
                {
                    if (s.KeyCode == Keys.Down)
                        crossroad.InitPanicMode();
                    else if (s.KeyCode == Keys.S)
                        crossroad.ShutUp();
                    else if (s.KeyCode == Keys.Up)
                        crossroad.InitRadio();
                };
                crossroad.FormClosed += delegate (object sender1, FormClosedEventArgs e1)
                {
                    this.Visible = true;
                    this.Select();
                    crossroad.ShutUp();
                };
            };
            perproad = new PerpRoad();
            perp.MouseClick += delegate (object sender, MouseEventArgs e)
            {
                perproad = new PerpRoad();
                perproad.Visible = true;
                perproad.InitImperialMarch();
                this.Visible = false;
                perproad.FormClosed += delegate (object sender1, FormClosedEventArgs e1)
                {
                    this.Visible = true;
                    this.Select();
                    perproad.ShutUp();
                };
            };
            

        }
        private static bool IsANumber(String s)
        {
            if (String.Empty == s)
                return false;
            for(int i=0;i<s.Length;i++)
            {
                if (!char.IsDigit(s[i]))
                    return false;
            }
            return true;
        }
        private void InitMain()
        {
            this.Size = new Size(600, 350);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "Choose Wisely";
            this.Icon = new Icon(Paths.iconPath);
            this.BackgroundImage = Image.FromFile(Paths.mainBackgroundPath);
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void InitPictureBoxes()
        {
            cross = new PictureBox();
            cross.Image = Image.FromFile(Paths.crossroadCroppedPath);
            cross.Size = new Size(150, 150);
            cross.Location = new Point(20, 50);
            cross.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(cross);



            perp = new PictureBox();
            perp.Image = Image.FromFile(Paths.perproadCroppedPath);
            perp.Size = new Size(150, 150);
            perp.Location = new Point(410, 50);
            perp.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(perp);
        }
        /*private void InitLabelsAndTextboxes()
        {

            periodTextbox = new TextBox();
            periodTextbox.Size = new Size(80, 50);
            periodTextbox.Location = new Point(320, 500);

            meanTextbox = new TextBox();
            meanTextbox.Size = new Size(80, 50);
            meanTextbox.Location = new Point(400, 500);

            this.Controls.Add(periodTextbox);
            this.Controls.Add(meanTextbox);


            meanLabel = new Label();
            meanLabel.Text = "Mean: ";
            meanLabel.Size = MalemNumerus.labelSize;
            meanLabel.Location = MalemNumerus.meanLabelLocation;
            meanLabel.BackColor = Color.Transparent;
            meanLabel.Font = new Font("Arial", 10);


            periodLabel = new Label();
            periodLabel.Text = "Period: ";
            periodLabel.Size = MalemNumerus.labelSize;
            periodLabel.Location = MalemNumerus.periodLabelLocation;
            periodLabel.BackColor = Color.Transparent;
            periodLabel.Font = new Font("Arial", 10);

            this.Select();
            this.Controls.Add(periodLabel);
            this.Controls.Add(meanLabel);
        } */
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if(e.KeyCode == Keys.Left)
            {
                crossroad = new Crossroad();
                crossroad.Visible = true;
                this.Visible = false;
                crossroad.FormClosed += delegate (object sender1, FormClosedEventArgs e1)
                {
                    this.Visible = true;
                };
            }
            else if(e.KeyCode == Keys.Right)
            {
                perproad = new PerpRoad();
                perproad.Visible = true;
                this.Visible = false;
                perproad.InitImperialMarch();
                perproad.FormClosed += delegate (object sender1, FormClosedEventArgs e1)
                {
                    this.Visible = true;
                    perproad.ShutUp();
                };
            }

        }
    }
}
