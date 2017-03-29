using System;

namespace FuelDistributors
{
    public class FuelTank
    {
        private double _pressureInTank;
        private double _temperature;

        public double FuelLevel;

        public FuelTank(double fuelLevel, double pressure, double temperature)
        {
            _pressureInTank = pressure;
            FuelLevel = fuelLevel;
            _temperature = temperature;
        }

        public void GenerateParamethers()
        {
            Random rnd = new Random();
            if (rnd.Next()%2 == 0)
            {
                _pressureInTank += rnd.NextDouble();
                _temperature += rnd.NextDouble();
            }
            else
            {
                _pressureInTank -= rnd.NextDouble();
                _temperature -= rnd.NextDouble();
            }
        }

        public bool IsSafe()
        {
            if (_pressureInTank >= 0.2 && _pressureInTank <= 4 && _temperature > 0 && _temperature < 25)
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
            _pressureInTank = 2.5;
            _temperature = 5.0;
        }
    }
}