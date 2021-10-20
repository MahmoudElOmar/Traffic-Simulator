using System;
using System.Collections.Generic;

namespace Traffic_Simulator
{
    using List = List<Spaceship>;
    public class SpaceRoad
    {
        int roadIndex;
        public SpaceTrafficLight stl;
        double mean, period;
        public List<double> ETA;
        public List spaceships;
        int nbSpaceships;
        public SpaceRoad(int roadIndex, double mean, double period)
        {
            this.roadIndex = roadIndex;
            this.mean = mean;
            this.period = period;
            stl = new SpaceTrafficLight(false, roadIndex);
            ETA = Road.expo(mean, period);
            nbSpaceships = ETA.Count;
            spaceships = new List(nbSpaceships);
            this.InitSpaceships();
            this.InitETA();

        }
        public int NbSpaceships
        {
            get
            {
                return this.nbSpaceships;
            }
        }
        private bool IsCloseEnough(Spaceship front, Spaceship back)
        {
            int diff = 0;
            if(front.Road == 1)
            {
                diff = Math.Abs(front.photo.Location.X - back.photo.Location.X);
            }
            else if(front.Road == 2)
            {
                if (front.HasGoneToDestination)
                    return false;
                diff = Math.Abs(front.photo.Location.Y - back.photo.Location.Y);
            }
            diff -= 110;
            return diff < MalemNumerus.spaceGap;
        }
        private void InitSpaceships()
        {
            for (int i = 0; i < nbSpaceships; i++)
                spaceships.Add(new Spaceship(roadIndex, this));
        }
        private void InitETA()
        {
            for (int i = 0; i < nbSpaceships; i++)
                spaceships[i].ETA = ETA[i] * 2500;
        }
        public void DriveSpaceshipsOnSpaceroad()
        {
            for(int i=0;i<nbSpaceships-1;i++)
            {
                if(spaceships[i].ETA <=MalemNumerus.spaceTimeElapsed)
                {
                    if(spaceships[i].IsOutSideForm())
                    {
                        spaceships[i].DriveSpaceship();
                        continue;
                    }
                    else
                    {
                        if (i == 0)
                            spaceships[i].DriveSpaceship();
                        if (!IsCloseEnough(spaceships[i], spaceships[i + 1]))
                            spaceships[i + 1].DriveSpaceship();
                        else
                            spaceships[i + 1].StopMoving();
                    }

                }
            }
        }
    }
}
