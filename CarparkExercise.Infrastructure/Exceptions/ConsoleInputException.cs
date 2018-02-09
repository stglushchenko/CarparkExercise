using System;

namespace CarparkExercise.Infrastructure.Exceptions
{
    public class ConsoleInputException: ApplicationException
    {
        public ConsoleInputException() : base()
        {
        }

        public ConsoleInputException(string message) : base(message)
        {
        }

        public ConsoleInputException(string message, Exception innerExeption) : base(message, innerExeption)
        {
        }
    }
}
