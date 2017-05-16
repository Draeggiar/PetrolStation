using System;

namespace FuelDistributors
{
    public class FuelTank
    {
        public double PressureInTank;
        public double Temperature;

        public double FuelLevel;

        public FuelTank(double fuelLevel, double pressure, double temperature)
        {
            PressureInTank = pressure;
            FuelLevel = fuelLevel;
            Temperature = temperature;
        }

        public void GenerateParamethers()
        {
            Random rnd = new Random();
            if (rnd.Next()%2 == 0)
            {
                PressureInTank += rnd.NextDouble();
                Temperature += rnd.NextDouble();
            }
            else
            {
                PressureInTank -= rnd.NextDouble();
                Temperature -= rnd.NextDouble();
            }
        }

        public bool IsSafe()
        {
            if (PressureInTank >= 0.2 && PressureInTank <= 4 && Temperature > 0 && Temperature < 25)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MakeSafe()
        {
            PressureInTank = 2.5;
            Temperature = 5.0;
        }
    }
}