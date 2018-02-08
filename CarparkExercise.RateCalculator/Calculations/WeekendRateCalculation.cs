using System;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Models.Enums;

namespace CarparkExercise.RateCalculator.Calculations
{
    public class WeekendRateCalculation : ICalculation
    {
        public bool AppliesTo(PayRateName payRateName) => payRateName == PayRateName.WeekendRate;

        public decimal Calculate(DateTime entryTime, DateTime exitTime) => 10m;

        public string Name => "Weekend Rate";
    }
}
