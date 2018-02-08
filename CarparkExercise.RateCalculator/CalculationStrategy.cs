using CarparkExercise.Infrastructure.Exceptions;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Models.Enums;
using System;
using System.Linq;

namespace CarparkExercise.RateCalculator
{
    public class CalculationStrategy : ICalculationStrategy
    {
        private ICalculation[] _calculationTypes;

        public CalculationStrategy(ICalculation[] calculationTypes)
        {
            _calculationTypes = calculationTypes;
        }

        private ICalculation GetCalculationType(PayRateName payRateName)
        {
            var result = _calculationTypes.FirstOrDefault(x => x.AppliesTo(payRateName));

            if (result == null)
            {
                throw new NotImplementedStrategyException($"The calculation strategy for {Enum.GetName(typeof(PayRateName),payRateName)} is not implemented");
            }

            return result;
        }

        public decimal Calculate(PayRateName payRateName, DateTime entryTime, DateTime exitTime)
        {
            return GetCalculationType(payRateName).Calculate(entryTime, exitTime);
        }

        public string GetName(PayRateName payRateName)
        {
            return GetCalculationType(payRateName).Name;
        }
    }
}
