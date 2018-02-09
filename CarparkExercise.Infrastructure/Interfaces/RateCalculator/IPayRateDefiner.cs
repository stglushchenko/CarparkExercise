using CarparkExercise.Models.Enums;
using System;

namespace CarparkExercise.Infrastructure.Interfaces.RateCalculator
{
    public interface IPayRateDefiner
    {
        PayRateName Define(DateTime entryTime, DateTime exitTime);
    }
}
