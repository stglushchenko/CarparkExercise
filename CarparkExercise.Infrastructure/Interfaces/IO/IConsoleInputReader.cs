using System;

namespace CarparkExercise.Infrastructure.Interfaces.IO
{
    public interface IConsoleInputReader
    {
        (DateTime entry, DateTime exit) Read();
    }
}
