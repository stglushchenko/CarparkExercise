using System;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Models.Enums;

namespace CarparkExercise.RateCalculator.Calculations
{
    public class NightRateCalculation : ICalculation
    {
        public bool AppliesTo(PayRateName payRateName) => payRateName == PayRateName.NightRate;

        public decimal Calculate(DateTime entryTime, DateTime exitTime) => 6.5m;

        public string Name => "Night Rate";
    }
}
