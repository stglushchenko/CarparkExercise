using CarparkExercise.Infrastructure.Exceptions;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Models.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CarparkExercise.RateCalculator
{
    public class CalculationStrategy : ICalculationStrategy
    {
        private ICalculation[] _calculationTypes;
        private readonly ILogger _logger;

        public CalculationStrategy(ICalculation[] calculationTypes, ILogger logger)
        {
            _calculationTypes = calculationTypes;
            _logger = logger;
        }

        private ICalculation GetCalculationType(PayRateName payRateName)
        {
            var result = _calculationTypes.FirstOrDefault(x => x.AppliesTo(payRateName));

            if (result == null)
            {
                var errorMessage = $"The calculation strategy for {Enum.GetName(typeof(PayRateName), payRateName)} is not implemented";
                _logger.LogError(errorMessage);
                throw new NotImplementedStrategyException(errorMessage);
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
