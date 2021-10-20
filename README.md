# Traffic-Simulator
A graphical traffic simulator that regulates circulation around a crossroad in accordance with traffic rules.

Each new car is generated according to a Poisson distribution, and is randomly assigned to a side in the crossroad. All cars drive with a constant velocity such as to represent being stuck in traffic. There are traffic lights to manage circulation. And as well as a collision detector that slows down when getting too close to a car, to avoid crashes (based on the assumption that each driver will slow down whenever they get too close to the car in front of them).

Project is coded on Visual Studio Community Edition with C#.
