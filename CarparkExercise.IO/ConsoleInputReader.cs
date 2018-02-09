using CarparkExercise.Infrastructure.Exceptions;
using CarparkExercise.Infrastructure.Interfaces.IO;
using Microsoft.Extensions.Logging;
using System;

namespace CarparkExercise.IO
{
    public class ConsoleInputReader : IConsoleInputReader
    {
        private readonly ILogger _logger;

        public ConsoleInputReader(ILogger logger)
        {
            _logger = logger;
        }

        public (DateTime entry, DateTime exit) Read()
        {
            return (ReadParameter("Entry date and time (yyyy-MM-dd hh:mm): "), ReadParameter("Exit date and time (yyyy-MM-dd hh:mm): "));
        }

        private DateTime ReadParameter(string inputMessage)
        {
            Console.Write(inputMessage);

            var input = Console.ReadLine();

            if (!DateTime.TryParse(input, out var result))
            {
                var errorMessage = $"Unable to parse the value {input}";
                _logger.LogError(errorMessage);
                throw new ConsoleInputException(errorMessage);
            }
            return result;
        }
    }
}
