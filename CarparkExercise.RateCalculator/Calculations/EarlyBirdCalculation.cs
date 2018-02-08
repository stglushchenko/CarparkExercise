using System;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Models.Enums;

namespace CarparkExercise.RateCalculator.Calculations
{
    public class EarlyBirdCalculation : ICalculation
    {
        public bool AppliesTo(PayRateName payRateName) => payRateName == PayRateName.EarlyBird;

        public decimal Calculate(DateTime entryTime, DateTime exitTime) => 13m;

        public string Name => "Early Bird";
    }
}
