using CarparkExercise.Models.Enums;
using System;

namespace CarparkExercise.Infrastructure.Interfaces.RateCalculator
{
    public interface ICalculation
    {
        decimal Calculate(DateTime entryTime, DateTime exitTime);

        string Name { get; }

        bool AppliesTo(PayRateName payRateName);
    }
}
