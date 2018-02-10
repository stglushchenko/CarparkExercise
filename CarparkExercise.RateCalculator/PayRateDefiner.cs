using System;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Models.Enums;
using static CarparkExercise.Models.Enums.PayRateName;
using NodaTime;
using static NodaTime.IsoDayOfWeek;

namespace CarparkExercise.RateCalculator
{
    public class PayRateDefiner : IPayRateDefiner
    {
        public PayRateName Define(DateTime entryTime, DateTime exitTime)
        {
            if (entryTime >= exitTime)
            {
                throw new ArgumentException("Entry date should be less than exit date");
            }

            var entry = LocalDateTime.FromDateTime(entryTime);
            var exit = LocalDateTime.FromDateTime(exitTime);

            if ((entry.DayOfWeek == Saturday || entry.DayOfWeek == Sunday)
                && (exit.DayOfWeek == Saturday || exit.DayOfWeek == Sunday)
                && Period.Between(entry, exit).Days <= 2)
            {
                return WeekendRate;
            }
            else if (entry.Date == exit.Date
                && entry.TimeOfDay >= new LocalTime(6, 0)
                && entry.TimeOfDay <= new LocalTime(9, 0)
                && exit.TimeOfDay >= new LocalTime(15, 30)
                && exit.TimeOfDay <= new LocalTime(23, 30))
            {
                return EarlyBird;
            }
            else if (entry.TimeOfDay >= new LocalTime(18, 0)
                && entry.TimeOfDay <= new LocalTime(23, 59, 59, 999)
                && entry.DayOfWeek != Saturday && entry.DayOfWeek != Sunday
                && exit <= entry.PlusDays(1).Date.AtMidnight().PlusHours(6))
            {
                return NightRate;
            }
            
            else
            {
                return StandardRate;
            }
        }
    }
}
