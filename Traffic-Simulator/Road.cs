using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Traffic_Simulator
{
    using List = List<Car>;
    public class Road
    {
        //Fields
        int roadIndex;
        public TrafficLight tl;
        double mean, period;
        public List<double> ETA;
        public List cars;
        public List<bool> born;
        public List<bool> addedToForm;
        Timer spy;
        int nbCars;
        int nbCarsOnRoad = 0;
        bool panicMode = false;
        int ambulaceCarIndex = 0;
        int nbCarsThatEnteredForm;

        //Constructor
        public Road(int roadIndex, double mean, double period)
        {
            this.roadIndex = roadIndex;
            this.mean = mean;
            this.period = period;

            tl = new TrafficLight(false, roadIndex);

            ETA = expo(mean, period);

            nbCars = ETA.Count;

            cars = new List(nbCars);

            InitBorn();
            InitAddedToForm();
            InitCarsOnRoad();
            InitETA();
            InitSpy();



        }

        //Private Mehods
        private void InitSpy()
        {
            spy = new Timer();
            spy.Enabled = true;
            spy.Interval = 100;
            spy.Start();

            spy.Tick += delegate (object sender, EventArgs e)
            {
                UpdateNbCarsOnRoad();
                UpdateNbCarsThatEnteredForm();
            };
        }

        private void InitETA()
        {
            for (int j = 0; j < nbCars; j++)
            {
                cars[j].ETA = ETA[j] * 2500;
            }
        }

        private void InitCarsOnRoad()
        {
            for (int i = 0; i < nbCars; i++)
            {
                cars.Add(new Car(roadIndex, this));
                //cars[i].photo.Image = Image.FromFile(Paths.carPhotosPath + i%18 + ".png");
            }
        }

        private void InitBorn()
        {
            born = new List<bool>(nbCars);
            for (int i = 0; i < nbCars; i++)
            {
                born.Add(false);
            }

        }

        private bool IsCloseEnough(Car front, Car back)
        {
            int diff = 0;
            if (front.Road != back.Road)
                throw new Exception();

            /*
            if ((front.Road == 2 || front.Road == 4) && front.HasGoneToDestination 
                && front.DestRoad % 2 != front.Road % 2
                && Math.Abs(front.photo.Location.X - back.photo.Location.X) > 0)
            {
                if(Math.Abs(front.photo.Location.Y - back.photo.Location.Y) <= 60)
                {
                    //MessageBox.Show("k");
                    return true;
                }
            }

            if ((front.Road == 1 || front.Road == 3) && front.HasGoneToDestination
                && Math.Abs(front.photo.Location.X - back.photo.Location.X) <= 60
                && Math.Abs(front.photo.Location.X - back.photo.Location.X) > 0
                && front.DestRoad % 2 != front.Road % 2)
                return true;
                */


            /*
            if (front.HasRotated)
            {
                if (back.Road == 1 || back.Road == 3)
                {
                    if (Math.Abs(front.photo.Location.X - back.photo.Location.X) <= 50)
                    {
                        if (Math.Abs(front.photo.Location.Y - back.photo.Location.Y) <= 150)
                        {
                            return true;
                        }
                    }
                }
            }
            */


            if (front.Road == 1 || front.Road == 3)
            {
                diff = Math.Abs(front.photo.Location.Y - back.photo.Location.Y);
            }
            else if (front.Road == 2 || front.Road == 4)
            {
                diff = Math.Abs(front.photo.Location.X - back.photo.Location.X);
            }


            if (front.HasGoneToDestination)
            {
                return false;
            }
            diff -= 80;
            return (diff < MalemNumerus.gap);
        }

        private void UpdateNbCarsOnRoad()
        {
            nbCarsOnRoad = 0;
            for (int i = 0; i < nbCars; i++)
            {
                if (cars[i].ETA < MalemNumerus.timeElapsed)
                {
                    if (!cars[i].HasGoneToDestination)
                        nbCarsOnRoad++;
                    else
                        nbCarsOnRoad--;
                }
            }
        }

        private void UpdateNbCarsThatEnteredForm()
        {
            nbCarsThatEnteredForm = 0;
            for (int i = 0; i < nbCars; i++)
            {
                if (cars[i].ETA < MalemNumerus.timeElapsed)
                {
                    nbCarsThatEnteredForm++;
                }
            }
        }

        public int AmbulanceCarIndex
        {
            get
            {
                return ambulaceCarIndex;
            }
            set
            {
                ambulaceCarIndex = value;
            }
        }

        public int RoadIndex
        {
            get
            {
                return this.roadIndex;
            }
        }

        public int NbCarsThatEnteredForm
        {
            get
            {
                return nbCarsThatEnteredForm;
            }
        }

        //Properties
        public bool PanicMode
        {
            get
            {
                return panicMode;
            }
            set
            {
                panicMode = value;
            }
        }

        public int NbCars
        {
            get
            {
                return nbCars;
            }
        }

        public int NbCarsOnRoad
        {
            get
            {
                return nbCarsOnRoad;
            }
        }

        private void InitAddedToForm()
        {
            addedToForm = new List<bool>(nbCars);
            for (int i = 0; i < nbCars; i++)
            {
                addedToForm.Add(false);
            }

        }

        public double Mean
        {
            get
            {
                return mean;
            }
            set
            {
                mean = value;
            }
        }

        public double Period
        {
            get
            {
                return period;
            }
            set
            {
                period = value;
            }
        }

        //Public methods
        public static List<double> expo(double mean, double period)
        {
            List<double> q = new List<double>();
            Random g = new Random();
            double current_time = 0.0, u, inter;
            while (current_time < period)
            {
                u = g.NextDouble(); // u in [0,1]
                inter = -1.0 / mean * Math.Log(1 - u);
                current_time += inter;
                if (current_time < period) q.Add(current_time);
            }
            return q;
        }

        public void DriveCarsOnRoad()
        {

            for (int i = 0; i < nbCars - 1; i++)
            {

                if (cars[i].ETA <= MalemNumerus.timeElapsed)
                {
                    //cars[i].DriveCar();



                    if (cars[i].IsOutsideForm())
                    {
                        cars[i].DriveCar();
                        continue;
                    }

                    else
                    {
                        if (i == 0)
                        {
                            cars[i].DriveCar();
                        }

                        if (!IsCloseEnough(cars[i], cars[i + 1]))
                        {
                            cars[i + 1].DriveCar();
                        }
                        else
                        {
                            cars[i + 1].StopMoving();
                        }
                    }
                }
            }
        }

        public void StopAll()
        {
            for (int i = 0; i < nbCars; i++)
            {
                cars[i].StopMoving();
            }

        }
    }
}