using System;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Models.Enums;

namespace CarparkExercise.RateCalculator.Calculations
{
    public class StandardRateCalculation : ICalculation
    {
        public bool AppliesTo(PayRateName payRateName) => payRateName == PayRateName.StandardRate;

        public decimal Calculate(DateTime entryTime, DateTime exitTime)
        {
            if (entryTime >= exitTime)
            {
                throw new ArgumentException("Entry date should be less than exit date");
            }

            var timeDifference = exitTime - entryTime;
            
            if (timeDifference <= TimeSpan.FromHours(1))
            {
                return 5m;
            }
            else if (timeDifference <= TimeSpan.FromHours(2))
            {
                return 10m;
            }
            else if (timeDifference <= TimeSpan.FromHours(3))
            {
                return 15m;
            }
            else
            {
                return ((exitTime.Date - entryTime.Date).Days + 1) * 20m;
            }
        }

        public string Name => "Standard Rate";
    }
}
