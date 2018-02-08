using System;

namespace CarparkExercise.Infrastructure.Exceptions
{
    public class NotImplementedStrategyException: ApplicationException
    {
        public NotImplementedStrategyException() : base()
        {
        }

        public NotImplementedStrategyException(string message) : base(message)
        {
        }

        public NotImplementedStrategyException(string message, Exception innerExeption) : base(message, innerExeption)
        {
        }
    }
}
