using CarparkExercise.Infrastructure.Interfaces.IO;
using CarparkExercise.Infrastructure.Interfaces.RateCalculator;
using CarparkExercise.Infrastructure.Interfaces.Workflows;
using Microsoft.Extensions.Logging;

namespace CarparkExercise.ConsoleApp
{
    public class MainWorkflow : IWorkflow
    {
        private readonly ILogger _logger;
        private readonly IConsoleInputReader _consoleInputReader;
        private readonly IConsoleResultWriter _consoleResultWriter;
        private readonly ICalculationStrategy _calculationStrategy;
        private readonly IPayRateDefiner _payRateDefiner;

        public MainWorkflow(ILogger logger,
            IConsoleInputReader consoleInputReader,
            IConsoleResultWriter consoleResultWriter,
            ICalculationStrategy calculationStrategy,
            IPayRateDefiner payRateDefiner)
        {
            _logger = logger;
            _consoleInputReader = consoleInputReader;
            _consoleResultWriter = consoleResultWriter;
            _calculationStrategy = calculationStrategy;
            _payRateDefiner = payRateDefiner;
        }

        public void Run()
        {
            var (entry, exit) = _consoleInputReader.Read();
            var payRateName = _payRateDefiner.Define(entry, exit);
            var writeableRateName = _calculationStrategy.GetName(payRateName);
            var totalCost = _calculationStrategy.Calculate(payRateName, entry, exit);
            _consoleResultWriter.Write(writeableRateName, totalCost);
        }
    }
}
