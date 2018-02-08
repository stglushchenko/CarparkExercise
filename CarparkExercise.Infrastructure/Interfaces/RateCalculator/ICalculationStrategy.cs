using CarparkExercise.Models.Enums;
using System;

namespace CarparkExercise.Infrastructure.Interfaces.RateCalculator
{
    public interface ICalculationStrategy
    {
        decimal Calculate(PayRateName payRateName, DateTime entryTime, DateTime exitTime);

        string GetName(PayRateName payRateName);
    }
}
