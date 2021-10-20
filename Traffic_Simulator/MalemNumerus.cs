using System.Drawing;


namespace Traffic_Simulator
{
    public class MalemNumerus
    {
        public static int formDimension = 750;
        //location of traffic lights

        public static int bigSide = 80;
        public static int smallSide = 40;


        //delay time whilst changing from red to yellow light
        public static int delay = 2000;

        
        public static Point r1 = new Point(281, 264);
        public static Point r2 = new Point(439, 268);
        public static Point r3 = new Point(437, 416);
        public static Point r4 = new Point(281, 414);

        public static Point y1 = new Point(281, 245);
        public static Point y2 = new Point(461, 267);
        public static Point y3 = new Point(437, 437);
        public static Point y4 = new Point(257, 414);

        public static Point g1 = new Point(281, 225);
        public static Point g2 = new Point(482, 267);
        public static Point g3 = new Point(437, 457);
        public static Point g4 = new Point(237, 414);

        // car initial points
        //to fix later on;
        public static Point sp1 = new Point(322,-90);
        public static Point sp2 = new Point(800,304);
        public static Point sp3 = new Point(371,770);
        public static Point sp4 = new Point(-90,352);


        //Zebraa threshold
        public static Point z1 = new Point(sp1.X,179);
        public static Point z2 = new Point(452,sp2.Y);
        public static Point z3 = new Point(sp3.X,432);
        public static Point z4 = new Point(205,sp4.Y);



        //Crossroad threshold
        public static Point left1 = new Point(z1.X,z2.Y+47);
        public static Point right1 = new Point(z1.X,z4.Y-47);

        public static Point left2 = new Point(z3.X-47,z2.Y);
        public static Point right2 = new Point(z1.X+50,z2.Y);

        public static Point left3 = new Point(z3.X,z4.Y-47);
        public static Point right3 = new Point(z3.X,z2.Y+50);

        public static Point left4 = new Point(z3.X,z4.Y);
        public static Point right4 = new Point(z1.X-3,z4.Y);


        public static double mean = 2;
        public static double period = 10;


        //speed
        public static int interval = 8;
        public static int speed = 2;

        public static int panicSpeed = 1;
        public static int panicInterval = 15;

        //time elapsed since execution
        public static double timeElapsed = 0;


        //gap between adjacent cars

        public static int gap = 70;


        //the vanisher to help put the cars outside the form

        public static int TheVanisher = 100;


        // space numbers locus numerus


        //spaceship start positions

        public static Point ssp1 = new Point(-125,457);
        public static Point ssp2 = new Point(314,-125);


        //space lights location

        public static Point sl1 = new Point(28, 598);
        public static Point sl2 = new Point(248, 196);


        //lightsaber dimenstions
        public static int lightSaberBigSide = 232;
        public static int lightSaberSmallSide = 33;


        //space speed and interval

        public static int spaceSpeed = 2;
        public static int spaceInterval = 8;


        //spaceship threshold

        public static Point sz1 = new Point(191, 465);
        public static Point sz2 = new Point(313, 336);

        //gap between spaceships

        public static int spaceGap = 70;

        //space timer elapsed in space road

        public static double spaceTimeElapsed = 0;
    }
}
